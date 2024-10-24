using AutoMapper;
using WebRoster.Models;
using WebRoster.Models.DTO;
using WebRoster.Utils.Mappers;
using FluentAssertions;
using Xunit;

public class MappingProfileTests {
    private readonly IMapper _mapper;
    public MappingProfileTests() {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void MapUserToUserDTO() {
        User user = new User() { FirstName = "", LastName = "", Password = "", UserName = ""};

        UserDTO userDTO = _mapper.Map<UserDTO>(user);

        userDTO.Should().NotBeNull();
        userDTO.FirstName.Should().Be(user.FirstName);
        userDTO.LastName.Should().Be(user.LastName);
    }

    
}