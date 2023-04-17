using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Domain.Models
{
    public class User
    {
        public Guid Id { get; init; }

        public string Login { get; init; }

        public string Password { get; init; }

        public string Name { get; init; }

        public int Gender { get; init; }

        public DateTime? Birthday { get; init; }

        public bool Admin { get; init; }

        public DateTime CreatedOn { get; init; }

        public string CreatedBy { get; init; }

        public DateTime ModifiedOn { get; init; }

        public string ModifiedBy { get; init;}

        public DateTime RevokedOn { get; init; }

        public string RevokedBy { get; init;}
    }
}
