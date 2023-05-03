using AtonTalent.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Domain.ComplexRequests
{
    public class CreateRequest
    {
        public LoginDto currentUser { get; set; } 
        public UserCreateDto userCreateDto { get; set; }
    }
}
