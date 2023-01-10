using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

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

    [HttpPost]
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO newUser)
    {
        var userCreated = await _userService.Register(newUser);
        if (userCreated == null)
            return BadRequest("There is an user registered whit that email. Please try another one");

        return Created("Usuario Creado", userCreated);
    }

}