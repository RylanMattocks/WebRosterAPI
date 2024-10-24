using WebRoster.Models.DTO;
using WebRoster.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebRoster.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : Controller{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService){
        this._courseService = courseService;
    }
    [HttpGet]
    public async Task<IActionResult> GetCourses(){
        try {
            return Ok(await _courseService.GetAllCoursesAsync());
        }
        catch{
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(int id){
        try{
            return Ok(await _courseService.GetCourseByIdAsync(id));
        }
        catch{
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCourse([FromBody] AddCourseDTO addCourseDTO){
        try {
            await _courseService.AddCourseAsync(addCourseDTO);
            return Created();
        }
        catch {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDTO updateCourseDTO){
        try {
            return Ok(await _courseService.UpdateCourseAsync(id, updateCourseDTO));
        }
        catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult>  DeleteCourse(int id){
        try{
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
        catch{
            return BadRequest();
        }
    }
}
