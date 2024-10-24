using AutoMapper;
using WebRoster.Models;
using WebRoster.Models.DTO;
namespace WebRoster.Utils.Mappers;

public class MappingProfile : Profile {
    public MappingProfile() {


        // User Mapping

        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName)).ReverseMap();

        CreateMap<User, AddUserDTO>().ReverseMap();

        CreateMap<User, UpdateUserDTO>().ReverseMap();

        // Course Mapping
        CreateMap<Course, CourseDTO>().ReverseMap();

        CreateMap<Course, AddCourseDTO>().ReverseMap();

        CreateMap<Course, UpdateCourseDTO>().ReverseMap();

        // Course Instructor Mapping

        CreateMap<CourseInstructor, CourseInstructorDTO>()
            .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.FirstName + " " + src.Instructor.LastName)).ReverseMap();

        CreateMap<CourseInstructor, AddCourseInstructorDTO>()
            .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.FirstName + " " + src.Instructor.LastName)).ReverseMap();

        // Course Student Mapping
        
        CreateMap<CourseStudent, CourseStudentDTO>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName))
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName)).ReverseMap();


        CreateMap<CourseStudent, AddCourseStudentDTO>()
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName)).ReverseMap();
        

        // Role Mapping
        CreateMap<Role, RoleDTO>().ReverseMap();

        CreateMap<Role, AddRoleDTO>().ReverseMap();

        CreateMap<Role, UpdateRoleDTO>().ReverseMap();


        

    }
}

