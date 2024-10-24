namespace WebRoster.Models.DTO;
public class UserDTO{
    public int ID { get; set; }
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public required string RoleName {get; set;}
}