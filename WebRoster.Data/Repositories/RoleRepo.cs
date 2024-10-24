using Microsoft.EntityFrameworkCore;
using WebRoster.Models;
namespace WebRoster.Data;
public class RoleRepo : IRoleRepo{
    private readonly RosterContext _context;
    public RoleRepo(RosterContext context){
        this._context = context;
    }
    public async Task<List<Role>> GetAllRolesAsync() {
        return await _context.Roles.ToListAsync();
    }
    public async Task<Role?> GetRoleByIdAsync(int id) {
        return await _context.Roles.FirstOrDefaultAsync(r => r.ID == id);
    }
    public async Task AddRoleAsync(Role role) {
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateRoleAsync(Role role) {
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteRoleAsync(Role role) {
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
    }
}