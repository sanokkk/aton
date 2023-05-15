using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Enums;
using AtonTalent.Domain.Models;

namespace AtonTalent.Services.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(LoginDto currentUser, UserCreateDto userCreateDto, CancellationToken cancellationToken);

    Task<User> UpdateAsync(LoginDto currentUser, UpdateUserDto updateModel, Guid id, CancellationToken cancellationToken);

    Task<User> ChangePasswordAsync(LoginDto currentUser, string newPassword, Guid id, CancellationToken cancellationToken);

    Task<User> ChangeLoginAsync(LoginDto currentUser, string newLogin, Guid id, CancellationToken cancellationToken);

    Task<User[]> GetActiveUsers(LoginDto currentUser, CancellationToken cancellationToken);

    Task<UserByLogin> GetByLoginAsync(LoginDto currentUser, string login, CancellationToken cancellationToken);

    Task<User> GetByLoginPass(LoginDto currentUser, LoginDto UserToGet, CancellationToken cancellationToken);

    Task<User[]> GetOverAge(LoginDto currentUser, int age, CancellationToken cancellationToken);

    Task<User> DeleteUserAsync(LoginDto currentUser, string login, DeleteType deleteType, CancellationToken cancellationToken);

    Task<User> RecoverUserAsync(LoginDto currentUser, Guid id, CancellationToken cancellationToken);

    Task<User> GetSelfUser(LoginDto currentUser, CancellationToken cancellationToken);
}
