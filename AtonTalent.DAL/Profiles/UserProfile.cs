using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Models;
using AutoMapper;

namespace AtonTalent.DAL.Profiles;

class UserProfile: Profile
 {
    public UserProfile() 
    {
        CreateMap<User, UserCreateDto>();
        CreateMap<UserCreateDto, User>();

        CreateMap<User, UserByLogin>()
            .ForMember(dst => dst.Name,
            opt =>
            {
                opt.MapFrom(src => src.Name);
            })
            .ForMember(dst => dst.Birthday,
            opt => opt.MapFrom(src => src.Birthday))
            .ForMember(dst => dst.Gender,
            opt => opt.MapFrom(src => src.Gender))
            .ForMember(dst => dst.IsActive,
            opt =>
            {
                opt.MapFrom(src => (src.RevokedOn == default(DateTime) ? true : false));
            });
    }
 }
