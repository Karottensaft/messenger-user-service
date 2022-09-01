using AutoMapper;
using UserMessengerService.Domain.Dto;
using UserMessengerService.Domain.Models;

namespace UserMessengerService.Application.Middlewares;

public class UserRegistrationProfile : Profile
{
    public UserRegistrationProfile()
    {
        CreateMap<RegistrationDto, UserModel>();
    }
}

public class UserInformationProfile : Profile
{
    public UserInformationProfile()
    {
        CreateMap<UserModel, InformationDto>();
    }
}

public class UserChangeProfile : Profile
{
    public UserChangeProfile()
    {
        CreateMap<ChangingDto, UserModel>();
    }
}