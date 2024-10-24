using AutoMapper;
using Moq;
using WebRoster.Data;
using WebRoster.Models;
using WebRoster.Models.DTO;
using WebRoster.Services;
using WebRoster.Utils.Mappers;
namespace WebRoster.TEST;

public class CourseServiceTests
{
    private readonly Mock<ICourseRepo> _mockRepo;
    private readonly IMapper _mapper;
    private readonly CourseService _courseService;
    public CourseServiceTests() {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = config.CreateMapper();
        _mockRepo = new Mock<ICourseRepo>();
        _courseService = new CourseService(_mockRepo.Object, _mapper);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    public async void GetAllCoursesReturnsList(int courses)
    {
        List<Course> courseList = [];
        for (int i = 0; i < courses; i++) {
            courseList.Add(new Course {CourseName = ""});
        }

        _mockRepo.Setup(repo => repo.GetAllCoursesAsync()).ReturnsAsync(courseList);

        var result = await _courseService.GetAllCoursesAsync();

        Assert.Equal(courses, result.Count);
    }

    [Theory]
    [InlineData(1, "math")]
    [InlineData(4, "english")]
    public async void GetCourseByIdReturnsCourse(int id, string name){
        List<Course> courseList = [
            new Course {ID = 1, CourseName = name},
            new Course {ID = 2, CourseName = ""},
            new Course {ID = 3, CourseName = ""},
            new Course {ID = 4, CourseName = name}
        ];

        _mockRepo.Setup(repo => repo.GetCourseByIdAsync(It.IsAny<int>())).ReturnsAsync(courseList.FirstOrDefault(c => c.ID == id));
        var result = await _courseService.GetCourseByIdAsync(id);

        Assert.Equal(result.CourseName, name);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(7)]
    public async void GetCourseByIdThrowsException(int id){
        List<Course> courseList = [
            new Course {ID = 1, CourseName = ""},
            new Course {ID = 2, CourseName = ""},
            new Course {ID = 3, CourseName = ""},
            new Course {ID = 4, CourseName = ""}
        ];

        _mockRepo.Setup(repo => repo.GetCourseByIdAsync(It.IsAny<int>())).ReturnsAsync(courseList.FirstOrDefault(c => c.ID == id));

        await Assert.ThrowsAnyAsync<NullReferenceException>(() => _courseService.GetCourseByIdAsync(id));
    }

    [Fact]
    public async void AddCourseToList(){
        List<Course> courseList = [
            new Course {ID = 1, CourseName = ""},
            new Course {ID = 2, CourseName = ""},
            new Course {ID = 3, CourseName = ""},
            new Course {ID = 4, CourseName = ""}
        ];

        AddCourseDTO addCourseDTO = new() {CourseName = "", InstructorName = ""};
        Course newCourse = new() {ID = 5, CourseName = ""};

        _mockRepo.Setup(repo => repo.AddCourseAsync(It.IsAny<Course>())).Callback(() => courseList.Add(newCourse));

        await _courseService.AddCourseAsync(addCourseDTO);

        Assert.Contains(courseList, c => c.ID == newCourse.ID);
    }

    [Theory]
    [InlineData(1, "math")]
    [InlineData(3, "english")]
    public async void UpdateCourseToList(int id, string newName){
        List<Course> courseList = [
            new Course {ID = 1, CourseName = ""},
            new Course {ID = 2, CourseName = ""},
            new Course {ID = 3, CourseName = ""},
            new Course {ID = 4, CourseName = ""}
        ];

        AddCourseInstructorDTO addCourseInstructorDTO = new() { InstructorName = ""};
        AddCourseStudentDTO addCourseStudentDTO = new() { StudentName = ""};
        List<AddCourseInstructorDTO> courseIs = [addCourseInstructorDTO];
        List<AddCourseStudentDTO> courseSs = [addCourseStudentDTO];

        UpdateCourseDTO updateCourseDTO = new() {CourseName = newName};
        Course newCourse = new() {ID = id, CourseName = newName};

        _mockRepo.Setup(repo => repo.UpdateCourseAsync(It.IsAny<Course>())).Callback(() => courseList.FirstOrDefault(c => c.ID == newCourse.ID)!.CourseName = newCourse.CourseName);
        _mockRepo.Setup(repo => repo.GetCourseByIdAsync(It.IsAny<int>())).ReturnsAsync(courseList.FirstOrDefault(c => c.ID == id));

        await _courseService.UpdateCourseAsync(id, updateCourseDTO);

        Assert.Equal(courseList[id - 1].CourseName, newName);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async void DeleteCourseFromList(int id){
        List<Course> courseList = [
            new Course {ID = 1, CourseName = ""},
            new Course {ID = 2, CourseName = ""},
            new Course {ID = 3, CourseName = ""},
            new Course {ID = 4, CourseName = ""}
        ];

        _mockRepo.Setup(repo => repo.DeleteCourseAsync(It.IsAny<Course>())).Callback(() => courseList.RemoveAll(c => c.ID == id));
        _mockRepo.Setup(repo => repo.GetCourseByIdAsync(It.IsAny<int>())).ReturnsAsync(courseList.FirstOrDefault(c => c.ID == id));

        await _courseService.DeleteCourseAsync(id);

        Assert.DoesNotContain(courseList, c => c.ID == id);
    }
}