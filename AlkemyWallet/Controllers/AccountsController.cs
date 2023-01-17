using AlkemyWallet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {
        var accounts = await _accountService.GetAllAsync();
        if (accounts is null)
            return BadRequest();

        return Ok(accounts);
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
    [Authorize(Roles ="Regular")]
    public async Task<IActionResult> Post()
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);

        var account = await _accountService.CreateAsync(userId);
        if (account is null)
            return BadRequest();

        return Ok(account);
    }
}