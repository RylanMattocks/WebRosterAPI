using System.Text.Json.Serialization;

namespace WebRoster.Models;
public class Course{
    public int ID {get; set;}
    public required string CourseName {get; set;}
    [JsonIgnore]
    public List<CourseInstructor> CourseInstructors {get; set;}
    [JsonIgnore]
    public List<CourseStudent> CourseStudents {get; set;}
}