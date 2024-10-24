using AutoMapper;
using Moq;
using WebRoster.Data;
using WebRoster.Models;
using WebRoster.Models.DTO;
using WebRoster.Services;
using WebRoster.Utils.Mappers;
namespace WebRoster.TEST;

public class RoleServiceTests
{
    private readonly Mock<IRoleRepo> _mockRepo;
    private readonly IMapper _mapper;
    private readonly RoleService _roleService;
    public RoleServiceTests(){
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = config.CreateMapper();
        _mockRepo = new Mock<IRoleRepo>();
        _roleService = new RoleService(_mockRepo.Object, _mapper);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    public async void GetAllRolesList(int roles)
    {
        List<Role> roleList = [];
        for (int i = 0; i < roles; i++) {
            roleList.Add(new Role {RoleName = ""});
        }

        _mockRepo.Setup(repo => repo.GetAllRolesAsync()).ReturnsAsync(roleList);
        var result = await _roleService.GetAllRolesAsync();

        Assert.Equal(roles, result.Count);
    }

    [Theory]
    [InlineData(1, "teach")]
    [InlineData(4, "stud")]
    public async void GetRoleByIdReturnsRole(int id, string name){
        List<Role> roleList = [
            new Role {ID = 1, RoleName = name},
            new Role {ID = 2, RoleName = ""},
            new Role {ID = 3, RoleName = ""},
            new Role {ID = 4, RoleName = name}
        ];

        _mockRepo.Setup(repo => repo.GetRoleByIdAsync(It.IsAny<int>())).ReturnsAsync(roleList.FirstOrDefault(r => r.ID == id));
        var result = await _roleService.GetRoleByIdAsync(id);

        Assert.Equal(name, result.RoleName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(7)]
    public async void GetRoleByIdThrowsException(int id){
        List<Role> roleList = [
            new Role {ID = 1, RoleName = ""},
            new Role {ID = 2, RoleName = ""},
            new Role {ID = 3, RoleName = ""},
            new Role {ID = 4, RoleName = ""}
        ];

        _mockRepo.Setup(repo => repo.GetRoleByIdAsync(It.IsAny<int>())).ReturnsAsync(roleList.FirstOrDefault(r => r.ID == id));

        await Assert.ThrowsAnyAsync<NullReferenceException>(() => _roleService.GetRoleByIdAsync(id));
    }

    [Fact]
    public async void AddRoleToList(){
        List<Role> roleList = [
            new Role {ID = 1, RoleName = ""},
            new Role {ID = 2, RoleName = ""},
            new Role {ID = 3, RoleName = ""},
            new Role {ID = 4, RoleName = ""}
        ];

        AddRoleDTO addRoleDTO = new AddRoleDTO() {RoleName = ""};
        Role newRole = new Role() {ID = 5, RoleName = ""};

        _mockRepo.Setup(repo => repo.AddRoleAsync(It.IsAny<Role>())).Callback(() => roleList.Add(newRole));

        await _roleService.AddRoleAsync(addRoleDTO);

        Assert.Contains(roleList, r => r.ID == newRole.ID);
    }

    [Theory]
    [InlineData(1, "teach")]
    [InlineData(3, "stud")]
    public async void UpdateRoleToList(int id, string newName) {
        List<Role> roleList = [
            new Role {ID = 1, RoleName = ""},
            new Role {ID = 2, RoleName = ""},
            new Role {ID = 3, RoleName = ""},
            new Role {ID = 4, RoleName = ""}
        ];

        UpdateRoleDTO updateRoleDTO = new() {RoleName = newName};
        Role newRole = new() {ID = id, RoleName = newName};

        _mockRepo.Setup(repo => repo.UpdateRoleAsync(It.IsAny<Role>())).Callback(() => roleList.FirstOrDefault(r => r.ID == newRole.ID)!.RoleName = newRole.RoleName);
        _mockRepo.Setup(repo => repo.GetRoleByIdAsync(It.IsAny<int>())).ReturnsAsync(roleList.FirstOrDefault(r => r.ID == id));

        await _roleService.UpdateRoleAsync(id, updateRoleDTO);

        Assert.Equal(newName, roleList[id - 1].RoleName);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async void DeleteRoleFromList(int id){
        List<Role> roleList = [
            new Role {ID = 1, RoleName = ""},
            new Role {ID = 2, RoleName = ""},
            new Role {ID = 3, RoleName = ""},
            new Role {ID = 4, RoleName = ""}
        ];

        _mockRepo.Setup(repo => repo.DeleteRoleAsync(It.IsAny<Role>())).Callback(() => roleList.RemoveAll(r => r.ID == id));
        _mockRepo.Setup(repo => repo.GetRoleByIdAsync(It.IsAny<int>())).ReturnsAsync(roleList.FirstOrDefault(r => r.ID == id));

        await _roleService.DeleteRoleAsync(id);

        Assert.DoesNotContain(roleList, r => r.ID == id);
    }
}