using AlkemyWallet.Core.Filters;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    //[HttpGet]
    //[Authorize(Roles = "Admin")]
    //public async Task<IActionResult> Get()
    //{
    //    try
    //    {
    //        var users = await _userService.GetAllAsync();
    //        return Ok(users);
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}

    /// <summary>
    /// Paged list of all registered users.  Only available for Administrators.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /users?page=1
    ///     
    /// </remarks>
    /// <returns>The list of Users</returns>
    /// <response code="200">All Users</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission</response>
    /// <response code="404">Users not found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoleDTO>))]
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
            var usersPage = await _userService.GetPaginated(validFilter.Page, validFilter.PageSize);
            if (usersPage is null)
                return BadRequest();

            return Ok(usersPage);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Get an user. Only available for Users.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /users/1
    ///     
    /// </remarks>
    /// <param name="id">User Id</param> 
    /// <returns>Role</returns>
    /// <response code="200">Return a role</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission</response>
    /// <response code="404">User not found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> GetById(int id)
    {
        // verificar que id sea del usuario logueado
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return BadRequest();

        return Ok(user);
    }

    /// <summary>
    /// Create an user.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /users
    ///     {
    ///       "firstName": "user",
    ///       "lastName": "test",
    ///       "email": "usertest@example.com",
    ///       "password": "Password@123"
    ///     }
    ///     
    /// </remarks>
    /// <param name="newUser"></param> 
    /// <returns>A newly created user</returns>
    /// <response code="200">Return a role</response>
    /// <response code="400">If the item is null</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserRegisteredDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost]
    //[Authorize(Roles = "Regular")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO newUser)
    {
        var userCreated = await _userService.Register(newUser);
        if (userCreated == null)
            return BadRequest("There is an user registered whit that email. Please try another one");

        return Created("User Created", userCreated);
    }

    /// <summary>
    /// Update an existing User.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /users/1
    ///     {
    ///        "firstName": "Name user",
    ///        "lastName": "LastName user",
    ///        "password": "Qwerty1234.,"
    ///     }
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="userDTO"></param>
    /// <returns>User Updated</returns>
    /// <response code="200">User was successfully updated.</response>
    /// <response code="400">information for update it is not valid</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission for update other users.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPut("{id}")]
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDTO userDTO)
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);
        User result = null;

        if (userId != id)
            return Forbid();

        try
        {
            result = await _userService.UpdateAsync(id, userDTO);

            if (result != null)
                return Ok(result);
            else
                return BadRequest(result);
        }
        catch(Exception ex) 
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Delete an existing User.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE /users/1
    ///     
    /// </remarks>
    /// <param name="id"></param>
    /// <returns>NoContent</returns>
    /// <response code="200">User was successfully deleted.</response>
    /// <response code="400">information for delete it is not valid</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission.</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var userDeleted = await _userService.DeleteAsync(id);
        if (userDeleted)
            return Ok("User " + id + " Deleted");
        else
            return NoContent();
    }

}