using System.Text.Json.Serialization;

namespace WebRoster.Models;
public class CourseStudent
{
    public int ID { get; set; }
    public int CourseID { get; set; }
    public int StudentID { get; set; }

    [JsonIgnore]
    public Course Course { get; set; }
    [JsonIgnore]
    public User Student { get; set; }
}