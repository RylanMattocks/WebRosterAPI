using WebRoster.Services;
using Microsoft.AspNetCore.Mvc;
using WebRoster.Models.DTO;
namespace WebRoster.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller{
    private readonly IUserService _userService;

    public UserController(IUserService userService) {
        this._userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers() {
        try {
            return Ok(await _userService.GetAllUsersAsync());
        }
        catch{
            return BadRequest();
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id) {
        try {
            return Ok(await _userService.GetUserByIdAsync(id));
        }
        catch {
            return BadRequest();
        }
    }
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] AddUserDTO addUserDTO){
        try {
            await _userService.AddUserAsync(addUserDTO);
            return Created();
        }
        catch {
            return BadRequest();
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO updateUserDTO){
        try {
            return Ok(await _userService.UpdateUserAsync(id, updateUserDTO));
        }
        catch {
            return NotFound();
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id){
        try{
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch{
            return BadRequest();
        }
    }
}
