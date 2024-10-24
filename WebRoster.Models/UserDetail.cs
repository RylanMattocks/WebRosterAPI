using System.Text.Json.Serialization;

namespace WebRoster.Models;
public class UserDetail{
    public int ID {get; set;}
    public int UserID {get; set;}
    public string Email {get; set;}
    public DateTime DateOfBirth {get; set;}
    public string Address {get; set;}
    [JsonIgnore]
    public User User {get; set;}
}