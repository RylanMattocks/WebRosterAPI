using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;
using WebRoster.Data;
using WebRoster.Models;
using WebRoster.Models.DTO;
using WebRoster.Utils;
using WebRoster.Utils.Generators;
namespace WebRoster.Services;
public class UserService : IUserService {
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;
    private readonly IGenerator _userGenerator;
    public UserService(IUserRepo userRepo, IMapper mapper, IGenerator userGenerator) {
        this._userRepo = userRepo;
        this._mapper = mapper;
        this._userGenerator = userGenerator;
    }
    public async Task<List<UserDTO>> GetAllUsersAsync() {
        var users = await _userRepo.GetAllUsersAsync();
        var userDTOs = _mapper.Map<List<UserDTO>>(users);
        return userDTOs;
    }
    public async Task<UserDTO> GetUserByIdAsync(int id) {
        User? user = await _userRepo.GetUserByIdAsync(id);
        if (user is null) throw new NullReferenceException();
        return _mapper.Map<UserDTO>(user);
    }
    public async Task AddUserAsync(AddUserDTO addUserDTO) {
        User user = _mapper.Map<User>(addUserDTO);
        user.UserName = _userGenerator.GenerateUsername(addUserDTO.FirstName + addUserDTO.LastName);
        user.Password = _userGenerator.GeneratePassword();
        await _userRepo.AddUserAsync(user);
    }
    public async Task<UserDTO> UpdateUserAsync(int id, UpdateUserDTO updateUserDTO) {

        User? user = await _userRepo.GetUserByIdAsync(id);
        if (user is null) throw new NullReferenceException();
        _mapper.Map(updateUserDTO, user);
        
        user.Password = _userGenerator.HashPassword(updateUserDTO.Password);

        await _userRepo.UpdateUserAsync(user);
        return _mapper.Map<UserDTO>(user);
    }
    public async Task DeleteUserAsync(int id) {
        User? user = await _userRepo.GetUserByIdAsync(id);
        if (user is null) throw new NullReferenceException();
        await _userRepo.DeleteUserAsync(user);
    }
}