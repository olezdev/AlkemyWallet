using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AlkemyWallet.Services.Interfaces;
using AlkemyWallet.Core.Filters;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Helper;
using AlkemyWallet.Entities;

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

    /// <summary>
    /// Paged list of all accounts. Only available for Administrators.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /accounts?page=1
    ///     
    /// </remarks>
    /// <returns>The list of Accounts</returns>
    /// <response code="200">All Accounts</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission</response>
    /// <response code="404">Accounts not found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionsDTO>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Get an account. Only available for Admin.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /accounts/1
    ///     
    /// </remarks>
    /// <param name="id">Account Id</param> 
    /// <returns>Account</returns>
    /// <response code="200">Return an account</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission</response>
    /// <response code="404">Account not found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDetailsDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var account = await _accountService.GetByIdAsync(id);
        if (account is null)
            return BadRequest();

        return Ok(account);
    }

    /// <summary>
    /// Create account.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /accounts
    ///     
    /// </remarks>
    /// <returns>Created Account</returns>
    /// <response code="200">A created account</response>
    /// <response code="400">If the item is null</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountCreatedDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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

    /// <summary>
    /// Update an existing Account.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PATCH /accounts/1
    ///     {
    ///       "isBlocked": "test"
    ///     }
    ///     
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="accountDTO"></param>
    /// <returns>IsBlocked Updated</returns>
    /// <response code="200">Account was successfully updated.</response>
    /// <response code="400">Information for update it is not valid</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountUpdatedDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPatch("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBlocked(int id, AccountToUpdateDTO accountDTO)
    {
        var account = await _accountService.UpdateBlockedAsync(id, accountDTO);
        if (account is null)
            return BadRequest();
        return Ok(account);
    }

    /// <summary>
    /// Delete an existing Account.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /accounts/1
    ///     
    /// </remarks>
    /// <param name="id"></param>
    /// <returns>NoContent</returns>
    /// <response code="200">Account was successfully deleted.</response>
    /// <response code="400">Information for delete it is not valid.</response>
    /// <response code="401">Invalid authentication credentials for the requested resource.</response>
    /// <response code="403">User has not permission.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var account = await _accountService.DeleteById(id);
        if (!account)
            return BadRequest();

        return NoContent();
    }

    /// <summary>
    /// Update Account and Create Deposit or Transfer Transaction .
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /accounts/1
    ///     {
    ///       "amount": "1000",
    ///       "type": "Deposito",
    ///       "concept": "test",
    ///       "toAccountId": "1"
    ///     }
    ///     
    ///     POST /accounts/1
    ///     {
    ///       "amount": "500",
    ///       "type": "Transferencia",
    ///       "concept": "test",
    ///       "toAccountId": "2"
    ///     }
    ///     
    /// </remarks>
    /// <returns>Created Account</returns>
    /// <response code="200">A created account</response>
    /// <response code="400">If the item is null</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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