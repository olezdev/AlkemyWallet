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

    [HttpPost("login")]
    public async Task<IActionResult> Post(LoginDTO loginDTO)
    {
        var result = await _authService.Login(loginDTO.Email, loginDTO.Password);
        if (result == null)
            return NoContent();

        return Ok(result);
    }

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