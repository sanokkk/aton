using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.DAL.Interfaces
{
    public interface IUserRepo
    {
        Task Add(User user);
        Task<User> GetByLoginPassAsync(LoginDto login, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateUserDto updateModel, User user, User requestedUser,CancellationToken cancellationToken);

        Task<User> GetByIdAsync(Guid id);

        Task ChangePasswordAsync(User user, string newPassword, User requestedUser, CancellationToken cancellationToken);
    }
}
