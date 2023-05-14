using AtonTalent.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Domain.ComplexRequests
{
    public class ChangePasswordRequest
    {
        [Required]
        public LoginDto CurrentUser { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
