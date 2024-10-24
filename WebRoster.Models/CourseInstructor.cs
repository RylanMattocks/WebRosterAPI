using System.Text.Json.Serialization;

namespace WebRoster.Models;
public class CourseInstructor
{
    public int ID { get; set; }
    public int CourseID { get; set; }
    public int InstructorID { get; set; }
    [JsonIgnore]
    public Course Course { get; set; }
    [JsonIgnore]
    public User Instructor { get; set; }
}