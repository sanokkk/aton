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

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public int Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public bool Admin { get; init; }

        public DateTime CreatedOn { get; init; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string? ModifiedBy { get; set;}

        public DateTime RevokedOn { get; set; }

        public string? RevokedBy { get; set;}
    }
}
