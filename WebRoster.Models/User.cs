using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WebRoster.Models;
public class User{
    [Key]
    // primary key
    public int ID {get; set;}
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public required string UserName {get; set;}
    public required string Password {get; set;}
    // foreign keys
    public int RoleID {get; set;}
    [JsonIgnore]
    public UserDetail UserDetail {get; set;}
    [JsonIgnore]
    public Role Role {get; set;}
    [JsonIgnore]
    public List<CourseInstructor> CourseInstructors {get; set;}
    [JsonIgnore]
    public List<CourseStudent> CourseStudents {get; set;}
}