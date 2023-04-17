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
        }
     }
}
