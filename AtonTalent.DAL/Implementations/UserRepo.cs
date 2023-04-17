using AtonTalent.DAL.Interfaces;
using AtonTalent.Domain.Models;
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
}
