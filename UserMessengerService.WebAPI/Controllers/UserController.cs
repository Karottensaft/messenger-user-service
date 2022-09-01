using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMessengerService.Application.Services;
using UserMessengerService.Domain.Dto;

namespace UserMessengerService.WebAPI.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(AuthenticationSchemes = "TokenKey", Roles = "admin")]
    [HttpGet("user/all")]
    public async Task<IEnumerable<InformationDto>> GetListOfUsers()
    {
        var users = await _userService.GerListOfUsers();
        return users;
    }

    [Authorize(AuthenticationSchemes = "TokenKey")]
    [HttpGet("user/me")]
    public async Task<InformationDto> GetCurrentUser()
    {
        var user = await _userService.GetCurrentUserByUsername();
        return user;
    }

    [Authorize(AuthenticationSchemes = "TokenKey")]
    [HttpGet("user/{username}")]
    public async Task<InformationDto> GetUser(string username)
    {
        var user = await _userService.GetUserByUsername(username);
        return user;
    }

    [Authorize(AuthenticationSchemes = "TokenKey")]
    [HttpPost("user/registration")]
    public async Task<RegistrationDto> PostUser(RegistrationDto userRegistrationData)
    {
        await _userService.CreateUser(userRegistrationData);
        return userRegistrationData;
    }

    [Authorize(AuthenticationSchemes = "TokenKey")]
    [HttpPut("user/user-settings")]
    public async Task<ChangingDto> PutUser(ChangingDto userChangingData)
    {
        await _userService.UpdateUser(userChangingData);
        return userChangingData;
    }

    [Authorize(AuthenticationSchemes = "TokenKey", Roles = "admin")]
    [HttpDelete("user/delete")]
    public async Task DeleteUser(string username)
    {
        await _userService.DeleteUser(username);
    }
}