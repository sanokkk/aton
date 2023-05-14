using AtonTalent.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Domain.ComplexRequests
{
    public class CreateRequest
    {
        [Required]
        public LoginDto currentUser { get; set; }

        [Required]
        public UserCreateDto userCreateDto { get; set; }
    }
}
