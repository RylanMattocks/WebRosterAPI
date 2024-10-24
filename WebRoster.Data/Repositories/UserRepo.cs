using Microsoft.EntityFrameworkCore;
using WebRoster.Models;
namespace WebRoster.Data;
public class UserRepo : IUserRepo{
    private readonly RosterContext _context;
    public UserRepo(RosterContext context){
        this._context = context;
    }
    public async Task<List<User>> GetAllUsersAsync() {
        //return await _context.Users.Include(u => u.UserDetail).Include(u => u.Role).ToListAsync();
        return await _context.Users.Include(u => u.Role).ToListAsync();
    }
    public async Task<User?> GetUserByIdAsync(int id) {
        return await _context.Users.Include(u => u.UserDetail).Include(u => u.Role).FirstOrDefaultAsync(u => u.ID == id);
    }
    public async Task AddUserAsync(User user) {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateUserAsync(User user) {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteUserAsync(User user) {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}