using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Services.Interfaces;
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
    public async Task<string> Post(LoginDTO loginDTO)
    {
        return await _authService.Login(loginDTO.Email, loginDTO.Password);
    }
}