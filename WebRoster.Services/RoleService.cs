using AutoMapper;
using WebRoster.Data;
using WebRoster.Models;
using WebRoster.Models.DTO;
namespace WebRoster.Services;
public class RoleService : IRoleService{
    private readonly IRoleRepo _roleRepo;
    private readonly IMapper _mapper;
    public RoleService(IRoleRepo roleRepo, IMapper mapper){
        this._roleRepo = roleRepo;
        this._mapper = mapper;
    }
    public async Task<List<RoleDTO>> GetAllRolesAsync() {
        var roles = await _roleRepo.GetAllRolesAsync();
        var rolesDTO = _mapper.Map<List<RoleDTO>>(roles);
        return rolesDTO;
    }
    public async Task<RoleDTO> GetRoleByIdAsync(int id) {
        Role? role = await _roleRepo.GetRoleByIdAsync(id);
        if (role is null) throw new NullReferenceException();
        return _mapper.Map<RoleDTO>(role);
    }
    public async Task AddRoleAsync(AddRoleDTO addRoleDTO) {
        Role role = _mapper.Map<Role>(addRoleDTO);
        await _roleRepo.AddRoleAsync(role);
    }
    public async Task<RoleDTO> UpdateRoleAsync(int id, UpdateRoleDTO updateRoleDTO) {
        Role? role = await _roleRepo.GetRoleByIdAsync(id);
        if (role is null) throw new NullReferenceException();
        _mapper.Map(updateRoleDTO, role);
        await _roleRepo.UpdateRoleAsync(role);
        return _mapper.Map<RoleDTO>(role);
    }
    public async Task DeleteRoleAsync(int id) {
        Role? role = await _roleRepo.GetRoleByIdAsync(id);
        if (role is null) throw new NullReferenceException();
        await _roleRepo.DeleteRoleAsync(role);
    }
}