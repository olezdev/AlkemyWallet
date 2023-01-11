using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers;

[Route("[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> Get()
    {
        var result = await _transactionService.GetAllAsync();
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);

        var transaction = await _transactionService.GetById(id, userId);
        if (transaction == null)
            return NoContent();
        
        return Ok(transaction);
    }
}
