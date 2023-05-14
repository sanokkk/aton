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
            var userRequest = await _userRepo.GetByLoginPassAsync(currentUser, cancellationToken);

            var createdUser = new User()
            {
                Id = Guid.NewGuid(),
                Login = userCreateDto.Login,
                Password = userCreateDto.Password,
                Name = userCreateDto.Name,
                Gender = userCreateDto.Gender,
                Admin = userRequest.Admin ? userRequest.Admin : false,
                Birthday = userCreateDto.Birthday,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userRequest.Login,
            };

            await _userRepo.Add(createdUser);
            return createdUser;
        }

        public async Task<User> UpdateAsync(LoginDto currentUser, UpdateUserDto updateModel, Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userRequested = await _userRepo.GetByLoginPassAsync(currentUser, cancellationToken);

            var user = await _userRepo.GetByIdAsync(id);

            if (userRequested.Admin || userRequested.Id == user.Id && userRequested.RevokedOn == default(DateTime))
            {
                await _userRepo.UpdateAsync(updateModel, user, userRequested, cancellationToken);
                return user;
            }

            else
            {
                throw new Exception($"User: {currentUser.Login} has no access.");
            }
        }

        public async Task<User> ChangePasswordAsync(LoginDto currentUser, string newPassword, Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userRequested = await _userRepo.GetByLoginPassAsync(currentUser, cancellationToken);

            var userToChangePassword = await _userRepo.GetByIdAsync(id);

            if (userRequested.Admin || userRequested.Id == userToChangePassword.Id && userRequested.RevokedOn == default(DateTime))
            {
                await _userRepo.ChangePasswordAsync(userToChangePassword, newPassword, userRequested, cancellationToken);
                return userToChangePassword;
            }
            else
            {
                throw new Exception($"User: {currentUser.Login} has no access.");
            }
        }

        public async Task<User> ChangeLoginAsync(LoginDto currentUser, string newLogin, Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userRequested = await _userRepo.GetByLoginPassAsync(currentUser, cancellationToken);

            var userToChangeLogin = await _userRepo.GetByIdAsync(id);

            if (userRequested.Admin || userRequested.Id == userToChangeLogin.Id && userRequested.RevokedOn == default(DateTime))
            {
                await _userRepo.ChangeLoginAsync(userToChangeLogin, newLogin, userRequested, cancellationToken);
                return userToChangeLogin;
            }
            else
            {
                throw new Exception($"User: {currentUser.Login} has no access.");
            }
        }

        public async Task<User[]> GetActiveUsers(LoginDto currentUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userRequested = await _userRepo.GetByLoginPassAsync(currentUser, cancellationToken);

            if (userRequested.Admin && userRequested.RevokedOn == default(DateTime))
            {
                return await _userRepo.GetUsersAsync();                
            }
            else
            {
                throw new Exception($"User: {currentUser.Login} has no access.");
            }
        }
    }
}
