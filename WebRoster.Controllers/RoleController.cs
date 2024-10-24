using WebRoster.Models;
using WebRoster.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Http;
using WebRoster.Models.DTO;
namespace WebRoster.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : Controller{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService) {
        this._roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles(){
        try{
            return Ok(await _roleService.GetAllRolesAsync());
        }
        catch{
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(int id) {
        try{
            return Ok(await _roleService.GetRoleByIdAsync(id));
        }
        catch{
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddRole([FromBody] AddRoleDTO addRoleDTO) {
        try{
            await _roleService.AddRoleAsync(addRoleDTO);
            return Created();
        }
        catch{
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleDTO updateRoleDTO) {
        try {
            return Ok(await _roleService.UpdateRoleAsync(id, updateRoleDTO));
        }
        catch {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id) {
        try {
            await _roleService.DeleteRoleAsync(id);
            return NoContent();
        }
        catch {
            return BadRequest();
        }
    }
}
