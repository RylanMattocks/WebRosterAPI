using WebRoster.Models;
using WebRoster.Models.DTO;

namespace WebRoster.Services;
public interface IUserService{
    public Task<List<UserDTO>> GetAllUsersAsync();
    public Task<UserDTO> GetUserByIdAsync(int id);
    public Task AddUserAsync(AddUserDTO addUserDTO);
    public Task<UserDTO> UpdateUserAsync(int id, UpdateUserDTO updateUserDTO);
    public Task DeleteUserAsync(int id);
}

public interface ICourseService{
    public Task<List<CourseDTO>> GetAllCoursesAsync();
    public Task<CourseDTO> GetCourseByIdAsync(int id);
    public Task AddCourseAsync(AddCourseDTO addCourseDTO);
    public Task<CourseDTO> UpdateCourseAsync(int id, UpdateCourseDTO updateCourseDTO);
    public Task DeleteCourseAsync(int id);
}

public interface IRoleService {
    public Task<List<RoleDTO>> GetAllRolesAsync();
    public Task<RoleDTO> GetRoleByIdAsync(int id);
    public Task AddRoleAsync(AddRoleDTO addRoleDTO);
    public Task<RoleDTO> UpdateRoleAsync(int id, UpdateRoleDTO updateRoleDTO);
    public Task DeleteRoleAsync(int id);
}