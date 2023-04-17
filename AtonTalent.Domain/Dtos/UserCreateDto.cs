using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Domain.Dtos
{
    public class UserCreateDto
    {
        public string Login { get; init; }

        public string Password { get; init; }

        public string Name { get; init; }

        public int Gender { get; init; }

        public bool IsAdmin { get; init; }
    }
}
