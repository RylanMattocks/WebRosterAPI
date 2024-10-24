using AutoMapper;
using Moq;
using WebRoster.Data;
using WebRoster.Models;
using WebRoster.Models.DTO;
using WebRoster.Services;
using WebRoster.Utils.Generators;
using WebRoster.Utils.Mappers;
namespace WebRoster.TEST;

public class UserServiceTests
{
    private readonly Mock<IUserRepo> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IGenerator> _generator;
    private readonly UserService _userService;
    public UserServiceTests(){
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = config.CreateMapper();
        _mockRepo = new Mock<IUserRepo>();
        _generator = new Mock<IGenerator>();
        _userService = new UserService(_mockRepo.Object, _mapper, _generator.Object);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    public async void GetAllUsersReturnsList(int users)
    {
        List<User> userList = [];

        for (int i = 0; i < users; i++) {
            userList.Add(new User {FirstName = "", LastName = "", UserName = "", Password = ""});
        }

        _mockRepo.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(userList);
        var result = await _userService.GetAllUsersAsync();

        Assert.Equal(users, result.Count);
    }

    [Theory]
    [InlineData(1, "john")]
    [InlineData(4, "jane")]
    public async void GetUserByIdReturnsUser(int id, string name){
        List<User> userList = [
            new User {ID = 1, FirstName = "john", LastName = "", UserName = "", Password = ""},
            new User {ID = 2, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 3, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 4, FirstName = "jane", LastName = "", UserName = "", Password = ""}
        ];

        _mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(userList.FirstOrDefault(u => u.ID == id));
        var result = await _userService.GetUserByIdAsync(id);

        Assert.Equal(name, result.FirstName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(7)]
    public async void GetUserByIdThrowsException(int id) {
        List<User> userList = [
            new User {ID = 1, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 2, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 3, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 4, FirstName = "", LastName = "", UserName = "", Password = ""}
        ];

        _mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(userList.FirstOrDefault(u => u.ID == id));

        await Assert.ThrowsAnyAsync<NullReferenceException>(() => _userService.GetUserByIdAsync(id));
    }

    [Fact]
    public async void AddUserToList() {
        List<User> userList = [
            new User {ID = 1, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 2, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 3, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 4, FirstName = "", LastName = "", UserName = "", Password = ""}
        ];

        AddUserDTO addUserDTO = new() {FirstName = "", LastName = ""};
        User user = new() {ID = 5, FirstName = "", LastName = "", UserName = "", Password = ""};

        _mockRepo.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).Callback(() => userList.Add(user));
        _generator.Setup(gen => gen.GenerateUsername(It.IsAny<string>())).Returns("username");
        _generator.Setup(gen => gen.GeneratePassword()).Returns("password");

        await _userService.AddUserAsync(addUserDTO);

        Assert.Contains(userList, u => u.ID == user.ID);
    }

    [Theory]
    [InlineData(1, "john")]
    [InlineData(3, "jane")]
    public async void UpdateUserToList(int id, string newName) {
        List<User> userList = [
            new User {ID = 1, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 2, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 3, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 4, FirstName = "", LastName = "", UserName = "", Password = ""}
        ];

        UpdateUserDTO updateUserDTO = new() {FirstName = newName, LastName = "", UserName = "", Password = ""};
        User newUser = new() {ID = 4, FirstName = newName, LastName = "", UserName = "", Password = ""};

        _mockRepo.Setup(repo => repo.UpdateUserAsync(It.IsAny<User>())).Callback(() => userList.FirstOrDefault(u => u.ID == id)!.FirstName = newUser.FirstName);
        _mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(userList.FirstOrDefault(u => u.ID == id));

        await _userService.UpdateUserAsync(id, updateUserDTO);

        Assert.Equal(newName, userList[id - 1].FirstName);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async void DeleteUserFromList(int id) {
        List<User> userList = [
            new User {ID = 1, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 2, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 3, FirstName = "", LastName = "", UserName = "", Password = ""},
            new User {ID = 4, FirstName = "", LastName = "", UserName = "", Password = ""}
        ];
        
        _mockRepo.Setup(repo => repo.DeleteUserAsync(It.IsAny<User>())).Callback(() => userList.RemoveAll(u => u.ID == id));
        _mockRepo.Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(userList.FirstOrDefault(u => u.ID == id));

        await _userService.DeleteUserAsync(id);
        
        Assert.DoesNotContain(userList, u => u.ID == id);
    }
}