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
    }
}
