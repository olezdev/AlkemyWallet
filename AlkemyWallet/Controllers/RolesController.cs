using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers;

[Route("[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// Gets the list of all roles. Only available for Administrators.
    /// </summary>
    /// <returns>The list of Roles</returns>
    /// <response code="200">All Roles in order</response>
    /// <response code="401">The client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
    /// <response code="403">When an no admin user try to use the endpoint</response>
    /// <response code="404">Source not found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoleDTO>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var roles = await _roleService.GetAllAsync();
            if(roles == null)
                return NoContent();

            return Ok(roles);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Get a role. Only available for Administrators.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET roles/{id}
    /// </remarks>
    /// <param name="id"></param> 
    /// <returns>Role</returns>
    /// <response code="200">Return a role</response>
    /// <response code="401">The client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
    /// <response code="403">When an no admin user try to use the endpoint</response>
    /// <response code="404">Source not found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDTO))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role is null)
                return NoContent();

            return Ok(role);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}