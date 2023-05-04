using AtonTalent.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Domain.ComplexRequests
{
    public class ChangePasswordRequest
    {
        public LoginDto CurrentUser { get; set; }

        public string NewPassword { get; set; }
    }
}
