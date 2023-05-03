using AtonTalent.DAL.Interfaces;
using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Models;
using AtonTalent.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Services.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> CreateUserAsync(LoginDto currentUser, UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var userRequest = await _userRepo.GetByLoginPassAsync(currentUser.Login, currentUser.Password, cancellationToken);

            var createdUser = new User()
            {
                Id = Guid.NewGuid(),
                Login = userCreateDto.Login,
                Password = userCreateDto.Password,
                Name = userCreateDto.Name,
                Gender = userCreateDto.Gender,
                Admin = userRequest.Admin ? userRequest.Admin : false,
                Birthday = userCreateDto.Birthday,
                CreatedOn = DateTime.Now,
                CreatedBy = userRequest.Login,
            };

            await _userRepo.Add(createdUser);
            return createdUser;
        }
    }
}
