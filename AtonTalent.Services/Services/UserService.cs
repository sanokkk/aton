using AtonTalent.DAL.Interfaces;
using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Enums;
using AtonTalent.Domain.Models;
using AtonTalent.Services.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
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

        public async Task<UserByLogin> GetByLoginAsync(LoginDto currentUser, string login, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userRequested = await _userRepo.GetByLoginPassAsync(currentUser, cancellationToken);

            if (userRequested.Admin)
            {
                return _mapper.Map<UserByLogin>(await _userRepo.GetByLoginAsync(login));
            }
            throw new Exception($"User: {currentUser.Login} has no access.");
        }

        public async Task<User> GetByLoginPass(LoginDto currentUser, LoginDto UserToGet, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (currentUser.Login == UserToGet.Login && currentUser.Password == UserToGet.Password)
            {
                return await _userRepo.GetByLoginPassAsync(currentUser, cancellationToken);
            }
            else 
                throw new Exception($"User: {currentUser.Login} has no access.");
        }

        public async Task<User[]> GetOverAge(LoginDto currentUser, int age, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userRequested = await _userRepo.GetByLoginPassAsync(currentUser, cancellationToken);

            if (userRequested.Admin)
            {
                return (await _userRepo.GetUsersAsync())
                    .Where(u => (DateTime.UtcNow - u.Birthday.Value).TotalDays / 365.25 > age)
                    .ToArray(); ;
            }
            else
                throw new Exception($"User: {currentUser.Login} has no access.");
        }

        public async Task<User> DeleteUserAsync(LoginDto currentUser, string login, DeleteType deleteType, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userRequested = await _userRepo.GetByLoginPassAsync(currentUser, cancellationToken);

            if (userRequested.Admin)
            {
                var userToDelete = await _userRepo.GetByLoginAsync(login);

                return (deleteType == DeleteType.Full) ?
                    await _userRepo.FullDelete(userToDelete, cancellationToken)
                    : await _userRepo.SoftDelete(userRequested, userToDelete, cancellationToken);
            }
            else
                throw new Exception($"User: {currentUser.Login} has no access.");
        }

        public async Task<User> RecoverUserAsync(LoginDto currentUser, Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userRequested = await _userRepo.GetByLoginPassAsync(currentUser, cancellationToken);

            if (userRequested.Admin)
            {
                var userToRecover = await _userRepo.GetByIdAsync(id);

                return await _userRepo.RecoverUserAsync(userToRecover, cancellationToken);
            }
            else
                throw new Exception($"User: {currentUser.Login} has no access.");
        }
    }
}
