namespace WebRoster.Models.DTO;

public class CourseDTO {
    public int ID { get; set; }
    public required string CourseName {get; set;}
    public required List<CourseInstructorDTO> CourseInstructors {get; set;}
}