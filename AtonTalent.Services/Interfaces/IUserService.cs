using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(LoginDto currentUser, UserCreateDto userCreateDto, CancellationToken cancellationToken);

        Task<User> UpdateAsync(LoginDto currentUser, UpdateUserDto updateModel, Guid id, CancellationToken cancellationToken);

        Task<User> ChangePasswordAsync(LoginDto currentUser, string newPassword, Guid id, CancellationToken cancellationToken);

        Task<User> ChangeLoginAsync(LoginDto currentUser, string newLogin, Guid id, CancellationToken cancellationToken);
    }
}
