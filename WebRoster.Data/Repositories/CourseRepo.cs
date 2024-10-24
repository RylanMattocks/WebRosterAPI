using Microsoft.EntityFrameworkCore;
using WebRoster.Models;
namespace WebRoster.Data;
public class CourseRepo : ICourseRepo{
    private readonly RosterContext _context;
    public CourseRepo(RosterContext context){
        this._context = context;
    }
    public async Task<List<Course>> GetAllCoursesAsync() {
        return await _context.Courses.Include(c => c.CourseInstructors).Include(c => c.CourseStudents).ToListAsync();
    }
    public async Task<Course?> GetCourseByIdAsync(int id) {
        return await _context.Courses.Include(c => c.CourseInstructors).Include(c => c.CourseStudents).FirstOrDefaultAsync(c => c.ID == id);
    }
    public async Task AddCourseAsync(Course course) {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateCourseAsync(Course course) {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteCourseAsync(Course course) {
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
    }
}