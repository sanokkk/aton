using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Models;

namespace AtonTalent.DAL.Interfaces;

public interface IUserRepo
{
    Task Add(User user);
    Task<User> GetByLoginPassAsync(LoginDto login, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateUserDto updateModel, User user, User requestedUser,CancellationToken cancellationToken);

    Task<User> GetByIdAsync(Guid id);

    Task ChangePasswordAsync(User user, string newPassword, User requestedUser, CancellationToken cancellationToken);

    Task ChangeLoginAsync(User user, string newLogin, User requestedUser, CancellationToken cancellationToken);

    Task<User[]> GetUsersAsync();

    Task<User> GetByLoginAsync(string login);

    Task<User> SoftDelete(User userRequested, User userToDelete, CancellationToken cancellationToken);

    Task<User> FullDelete(User userToDelete, CancellationToken cancellationToken);

    Task<User> RecoverUserAsync(User userToRecover, CancellationToken cancellationToken);
}
