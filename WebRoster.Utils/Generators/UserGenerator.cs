using WebRoster.Data;
namespace WebRoster.Utils.Generators;
public class UserGenerator : IGenerator{
    private readonly RosterContext _context;
    public UserGenerator(RosterContext context) {
        this._context = context;
    }
    
    public string GenerateUsername(string name){
        int userCount = _context.Users.Count() + 1;
        return $"{name}{userCount}";
    }

    public string GeneratePassword(){
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
        var random = new Random();
        var password = new char[10];

        for (int i = 0; i < 10; i++) {
            password[i] = validChars[random.Next(validChars.Length)];
        }
        return new string(password);
    }

    public string HashPassword(string password){
        return password;
    }
}