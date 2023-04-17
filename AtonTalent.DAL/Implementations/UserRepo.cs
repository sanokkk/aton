using AtonTalent.DAL.Interfaces;
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

    public async Task<User> GetByLoginPassAsync(string login, string password, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var response = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
        if (response == null)
            throw new ArgumentNullException(nameof(response));
        return response;
    }
}
