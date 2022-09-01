using UserMessengerService.Domain.Dto;

namespace UserMessengerService.Application.Services;

public interface IUserService
{
    Task<IEnumerable<InformationDto>> GerListOfUsers();

    Task<InformationDto> GetCurrentUserByUsername();

    Task<InformationDto> GetUserByUsername(string username);

    Task CreateUser(RegistrationDto userToMap);

    Task UpdateUser(ChangingDto userChanged);

    Task DeleteUser(string username);
}