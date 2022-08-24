using Microsoft.AspNetCore.Mvc;
using UserMessengerService.Application.Services;
using UserMessengerService.Domain.Dto;

namespace UserMessengerService.WebAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("user/all")]
        public async Task<IEnumerable<InformationDto>> GetListOfUsers()
        {
            var users = await _userService.GerListOfUsers();
            return users;
        }

        [HttpGet("user/me")]
        public async Task<InformationDto> GetCurrentUser(int id)
        {
            var user = await _userService.GetCurrentUser(id);
            return user;
        }

        [HttpGet("user/{username}")]
        public async Task<InformationDto> GetUser(string username)
        {
            var user = await _userService.GetUserByUsername(username);
            return user;
        }

        [HttpPost("user/registration")]
        public async Task<RegistrationDto> PostUser(RegistrationDto userRegistrationData)
        {
            await _userService.CreateUser(userRegistrationData);
            return userRegistrationData;
        }

        [HttpPut("user/user-settings")]
        public async Task<ChangingDto> PutUser(ChangingDto userChangingData, int id)
        {
            await _userService.UpdateUser(userChangingData, id);
            return userChangingData;
        }

        [HttpDelete("user/delete")]
        public async Task DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
        }
    }
}
