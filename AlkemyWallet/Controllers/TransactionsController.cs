using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Services.Interfaces;
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
	public async Task<IActionResult> Get()
	{
		return Ok(await _transactionService.GetAllAsync());
	}
}
