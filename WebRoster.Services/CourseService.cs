using AutoMapper;
using WebRoster.Data;
using WebRoster.Models;
using WebRoster.Models.DTO;
namespace WebRoster.Services;
public class CourseService : ICourseService{
    private readonly ICourseRepo _courseRepo;
    private readonly IMapper _mapper;
    public CourseService(ICourseRepo courseRepo, IMapper mapper){
        this._courseRepo = courseRepo;
        this._mapper = mapper;
    }
    public async Task<List<CourseDTO>> GetAllCoursesAsync() {
        var courses = await _courseRepo.GetAllCoursesAsync();
        var coursesDTO = _mapper.Map<List<CourseDTO>>(courses);
        return coursesDTO;
    }
    public async Task<CourseDTO> GetCourseByIdAsync(int id) {
        Course? course = await _courseRepo.GetCourseByIdAsync(id);
        if (course is null) throw new NullReferenceException();
        return _mapper.Map<CourseDTO>(course);
    }
    public async Task AddCourseAsync(AddCourseDTO addCourseDTO) {
        Course course = _mapper.Map<Course>(addCourseDTO);
        await _courseRepo.AddCourseAsync(course);
    }
    public async Task<CourseDTO> UpdateCourseAsync(int id, UpdateCourseDTO updateCourseDTO) {
        Course? course = await _courseRepo.GetCourseByIdAsync(id);
        if (course is null) throw new NullReferenceException();
        _mapper.Map(updateCourseDTO, course);
        await _courseRepo.UpdateCourseAsync(course);
        return _mapper.Map<CourseDTO>(course);
    }
    public async Task DeleteCourseAsync(int id) {
        Course? course = await _courseRepo.GetCourseByIdAsync(id);
        if (course is null) throw new NullReferenceException();
        await _courseRepo.DeleteCourseAsync(course);
    }
}