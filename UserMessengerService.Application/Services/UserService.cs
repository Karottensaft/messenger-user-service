using AutoMapper;
using UserMessengerService.Domain.Dto;
using UserMessengerService.Domain.Models;
using UserMessengerService.Infrastructure.Repositories;

namespace UserMessengerService.Application.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InformationDto>> GerListOfUsers()
        {
            var usersToMap = await _unitOfWork.UserRepository.GetEntityListAsync();
            var users = _mapper.Map<IEnumerable<InformationDto>>(usersToMap);
            return users;
        }

        public async Task<InformationDto> GetCurrentUser(int id)
        {
            var usersToMap = await _unitOfWork.UserRepository.GetEntityByIdAsync(id);
            return _mapper.Map<InformationDto>(usersToMap);
        }

        public async Task<InformationDto> GetUserByUsername(string username)
        {
            var userToMap = await _unitOfWork.UserRepository.GetEntityByNameAsync(username);
            return _mapper.Map<InformationDto>(userToMap);
        }


        public async Task CreateUser(RegistrationDto userToMap)
        {
            var userToValidate = await _unitOfWork.UserRepository.GetEntityByNameAsync(userToMap.Username);

            if (userToValidate == null)
            {
                var user = _mapper.Map<UserModel>(userToMap);
                user.ChangingDate = DateTime.Now;
                user.CreationDate = DateTime.Now;
                _unitOfWork.UserRepository.PostEntity(user);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new ArgumentException("User already exist.");
            }
        }

        public async Task UpdateUser(ChangingDto userChanged, int id)
        {
            var userToChange = await _unitOfWork.UserRepository.GetEntityByIdAsync(id);
            userToChange.ChangingDate = DateTime.Now;
            _mapper.Map(userChanged, userToChange);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUser(int id)
        {
            _unitOfWork.UserRepository.DeleteEntity(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
