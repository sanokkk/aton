using AtonTalent.DAL.Interfaces;
using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.DAL.Implementations;

public class UserRepo : IUserRepo
{
    private readonly ApplicationDbContext _db;

    public UserRepo(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Add(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(Guid id) => await _db.Users.FirstOrDefaultAsync(f => f.Id == id);
   

    public async Task<User> GetByLoginPassAsync(LoginDto login, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var response = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login.Login && u.Password == login.Password);
        if (response == null)
            throw new ArgumentNullException(nameof(response));
        return response;
    }

    public async Task UpdateAsync(UpdateUserDto updateModel, User user, User requestedUser, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        user.Name = updateModel.Name ?? user.Name;
        user.Gender = updateModel.Gender ?? user.Gender;
        user.Birthday = updateModel.Birthday ?? user.Birthday;

        user.ModifiedBy = requestedUser.Login;
        user.ModifiedOn = DateTime.UtcNow;

        await _db.SaveChangesAsync();
    }

    public async Task ChangePasswordAsync(User user, string newPassword, User requestedUser, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        user.Password = newPassword;

        user.ModifiedBy = requestedUser.Login;
        user.ModifiedOn = DateTime.UtcNow;

        await _db.SaveChangesAsync();
    }

    public async Task ChangeLoginAsync(User user, string newLogin, User requestedUser, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        user.Login = newLogin;

        user.ModifiedBy = requestedUser.Login;
        user.ModifiedOn = DateTime.UtcNow;

        await _db.SaveChangesAsync();
    }

    public async Task<User[]> GetUsersAsync() => await _db.Users
        .Where(u => u.RevokedOn == default(DateTime))
        .OrderBy(o => o.CreatedOn)
        .ToArrayAsync();

    public async Task<User> GetByLoginAsync(string login) => await _db.Users.FirstOrDefaultAsync(f => f.Login == login);

    public async Task<User> SoftDelete(User userRequested, User userToDelete, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        userToDelete.RevokedOn = DateTime.UtcNow;
        userToDelete.RevokedBy = userRequested.Login;

        await _db.SaveChangesAsync();

        return userToDelete;
    }

    public async Task<User> FullDelete(User userToDelete, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _db.Users.Remove(userToDelete);

        await _db.SaveChangesAsync();

        return userToDelete;
    }

    public async Task<User> RecoverUserAsync(User userToRecover, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        userToRecover.RevokedOn = default(DateTime);
        userToRecover.RevokedBy = default(string);

        await _db.SaveChangesAsync();

        return userToRecover;
    }
}
