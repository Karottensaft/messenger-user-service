using AutoMapper;
using UserMessengerService.Application.Middlewares;
using UserMessengerService.Domain.Dto;
using UserMessengerService.Domain.Models;
using UserMessengerService.Infrastructure.Repositories;

namespace UserMessengerService.Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;
    private readonly IUserProviderMiddleware _userProviderMiddleware;

    public UserService(UnitOfWork unitOfWork, IMapper mapper, IUserProviderMiddleware userProviderMiddleware)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userProviderMiddleware = userProviderMiddleware;
    }

    public async Task<IEnumerable<InformationDto>> GerListOfUsers()
    {
        var usersToMap = await _unitOfWork.UserRepository.GetEntityListAsync();
        var users = _mapper.Map<IEnumerable<InformationDto>>(usersToMap);
        return users;
    }

    public async Task<InformationDto> GetCurrentUserByUsername()
    {
        var username = _userProviderMiddleware.GetUsername();
        var userToMap = await _unitOfWork.UserRepository.GetEntityByNameAsync(username);
        return _mapper.Map<InformationDto>(userToMap);
    }

    public async Task<InformationDto> GetUserByUsername(string username)
    {
        var userToMap = await _unitOfWork.UserRepository.GetEntityByNameAsync(username);
        return _mapper.Map<InformationDto>(userToMap);
    }


    public async Task CreateUser(RegistrationDto userToMap)
    {
        var user = _mapper.Map<UserModel>(userToMap);
        user.Username = _userProviderMiddleware.GetUsername();
        user.ChangingDate = DateTime.Now;
        user.CreationDate = DateTime.Now;
        _unitOfWork.UserRepository.PostEntity(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateUser(ChangingDto userChanged)
    {
        var username = _userProviderMiddleware.GetUsername();
        var userToChange = await _unitOfWork.UserRepository.GetEntityByNameAsync(username);
        userToChange.ChangingDate = DateTime.Now;
        _mapper.Map(userChanged, userToChange);

        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteUser(string username)
    {
        _unitOfWork.UserRepository.DeleteEntity(username);
        await _unitOfWork.SaveAsync();
    }
}