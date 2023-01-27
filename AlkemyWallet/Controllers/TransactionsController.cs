using AlkemyWallet.Core.Filters;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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


    /// <summary>
    /// Paged list of all transactions. Only available for Administrators.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /transactions?page=1
    ///     
    /// </remarks>
    /// <returns>The list of Transactions</returns>
    /// <response code="200">All Transactions</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission</response>
    /// <response code="404">Transactions not found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionsDTO>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllPaged([FromQuery] PaginationFilter filter)
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

    /// <summary>
    /// Get a transaction. Only available for Users.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /transactions/1
    ///     
    /// </remarks>
    /// <param name="id">Transaction Id</param> 
    /// <returns>Role</returns>
    /// <response code="200">Return a transaction</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission</response>
    /// <response code="404">Transaction not found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionDetailsDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Create transaction.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /transactions
    ///     {
    ///       "Amount": "1000",
    ///       "Concept": "test",
    ///       "Type": "Deposito",
    ///       "UserId": "2",
    ///       "AccountId": "1",
    ///       "ToAccountId": "1"
    ///     }
    ///     
    /// </remarks>
    /// <param name="transactionDTO"></param> 
    /// <returns>Created Transaction</returns>
    /// <response code="200">A created transaction</response>
    /// <response code="400">If the item is null</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionCreatedDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost]
    //[Authorize(Roles = "Regular")]
    public async Task<IActionResult> Post([FromBody] TransactionToCreateDTO transactionDTO)
    {
        var transaction = await _transactionService.CreateAsync(transactionDTO);

        if (transaction == null)
            return BadRequest();

        return Created("Transaction Created", transaction);
    }

    /// <summary>
    /// Update an existing Transaction.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PATCH /transactions/1
    ///     {
    ///       "concept": "test"
    ///     }
    ///     
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="transactionDTO"></param>
    /// <returns>Concept Updated</returns>
    /// <response code="200">Transaction was successfully updated.</response>
    /// <response code="400">Information for update it is not valid</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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

    /// <summary>
    /// Delete an existing Transaction.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /transactions/1
    ///     
    /// </remarks>
    /// <param name="id"></param>
    /// <returns>NoContent</returns>
    /// <response code="200">Transaction was successfully deleted.</response>
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
        var transactionDeleted = await _transactionService.DeleteAsync(id);
        if (transactionDeleted)
            return Ok(transactionDeleted);

        return BadRequest();
    }
}
