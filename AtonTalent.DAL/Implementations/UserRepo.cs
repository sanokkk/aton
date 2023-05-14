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
}
