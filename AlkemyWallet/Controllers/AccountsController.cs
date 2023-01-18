using AlkemyWallet.Core.Filters;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using AlkemyWallet.Core.Helper;

namespace AlkemyWallet.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IHttpClientFactory _httpClientFactory;

    public AccountsController(IAccountService accountService, IHttpClientFactory httpClientFactory)
    {
        _accountService = accountService;
        _httpClientFactory = httpClientFactory;
    }

    //[HttpGet]
    //[Authorize(Roles = "Admin")]
    //public async Task<IActionResult> Get()
    //{
    //    var accounts = await _accountService.GetAllAsync();
    //    if (accounts is null)
    //        return BadRequest();

    //    return Ok(accounts);
    //}

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
    {
        try
        {
            var validFilter = new PaginationFilter(filter.Page, filter.PageSize);
            var accountsPage = await _accountService.GetPaginated(validFilter.Page, validFilter.PageSize);
            if (accountsPage is null)
                return BadRequest();

            return Ok(accountsPage);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var account = await _accountService.GetByIdAsync(id);
        if (account is null)
            return BadRequest();

        return Ok(account);
    }

    [HttpPost]
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> Post()
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);

        var account = await _accountService.CreateAsync(userId);
        if (account is null)
            return BadRequest();

        return Ok(account);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBlocked(int id, AccountToUpdateDTO accountDTO)
    {
        var account = await _accountService.UpdateBlockedAsync(id, accountDTO);
        if (account is null)
            return BadRequest();
        return Ok(account);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var account = await _accountService.DeleteById(id);
        if (!account)
            return BadRequest();

        return NoContent();
    }

    [HttpPost("{id}")]
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> TransactionAsync(int id, TransactionDTO transactionDTO)
    {
        if (transactionDTO.Amount <= (decimal)0.01)
            return BadRequest("Amount must be greater than 0,01");

        var userId = int.Parse(User.FindFirst("UserId").Value);
        var accountResponse = await _accountService.VerifyAccountAsync(id, userId, transactionDTO);

        if (accountResponse is null)
            return BadRequest("Please verify destination account");

        var launchUrl = LaunchUrl.GetApplicationUrl();
        var httpClient = _httpClientFactory.CreateClient("transactions");

        // add jwt to httpClient
        var httpResponseMessage = await httpClient.PostAsJsonAsync(launchUrl + "/transactions", transactionDTO);
        var data = await httpResponseMessage.Content.ReadAsStringAsync();

        return Ok(accountResponse);

    }
}