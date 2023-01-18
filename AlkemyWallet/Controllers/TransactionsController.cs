using AlkemyWallet.Core.Filters;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Services;
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

    //[HttpGet]
    //[Authorize(Roles = "Regular")]
    //public async Task<IActionResult> GetAll()
    //{
    //    var result = await _transactionService.GetAllAsync();
    //    if (result == null)
    //        return NotFound();

    //    return Ok(result);
    //}

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
    {
        try
        {
            var validFilter = new PaginationFilter(filter.Page, filter.PageSize);
            var transactionsPage = await _transactionService.GetPaginated(validFilter.Page, validFilter.PageSize);
            if (transactionsPage is null)
                return BadRequest();

            return Ok(transactionsPage);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}")]
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);

        var transaction = await _transactionService.GetByIdAsync(id, userId);
        if (transaction == null)
            return NoContent();

        return Ok(transaction);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post([FromBody] TransactionToCreateDTO transactionDTO)
    {
        var transaction = await _transactionService.CreateAsync(transactionDTO);

        if (transaction == null)
            return BadRequest();

        return Created("Transaction Created", transaction);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> Patch(int id, [FromBody] TransactionToUpdateDTO transactionDTO)
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);

        try
        {
            var result = await _transactionService.UpdateAsync(id, userId, transactionDTO);

            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var transactionDeleted = await _transactionService.DeleteAsync(id);
        if (transactionDeleted)
            return Ok(transactionDeleted);
        return BadRequest();
    }
}
