using WebRoster.Data;
namespace WebRoster.Utils.Generators;
public interface IGenerator{
    public string GenerateUsername(string name);
    public string GeneratePassword();
    public string HashPassword(string password);
}
