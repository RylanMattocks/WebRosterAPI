namespace WebRoster.Models.DTO;

public class AddUserDTO {
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int RoleID { get; set; }
}