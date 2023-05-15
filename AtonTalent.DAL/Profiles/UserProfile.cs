using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.DAL.Profiles
{
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
}
