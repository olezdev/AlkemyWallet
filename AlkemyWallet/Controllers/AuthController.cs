using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Get JWT with user and password
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /auth/login
    ///     {
    ///       "email": "usertest@example.com",
    ///       "password": "Password@123"
    ///     }
    ///     
    /// </remarks>
    /// <returns>JWT</returns>
    /// <response code="200">Json Web Token</response>
    /// <response code="400">If the item is null</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("login")]
    public async Task<IActionResult> Post(LoginDTO loginDTO)
    {
        var result = await _authService.Login(loginDTO.Email, loginDTO.Password);
        if (result == null)
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Get a profile.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /auth/me
    ///     
    /// </remarks>
    /// <returns>User Profile</returns>
    /// <response code="200">Return a profile</response>
    /// <response code="401">Invalid authentication credentials for the requested resource</response>
    /// <response code="403">User has not permission</response>
    /// <response code="404">User not found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDetailsDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("me")]
    [Authorize(Roles = "Admin, Regular")]
    public async Task<IActionResult> Me()
    {
        var id = int.Parse(User.FindFirst("UserId").Value);
        var result = await _authService.GetProfile(id);
        if (result == null)
            return NoContent();

        return Ok(result);
    }
}