using System.Text.Json.Serialization;

namespace WebRoster.Models;
public class Role{
    public int ID {get; set;}
    public required string RoleName {get; set;}
    [JsonIgnore]
    public List<User> Users {get; set;}
}