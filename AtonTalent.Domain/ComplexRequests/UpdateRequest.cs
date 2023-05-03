using AtonTalent.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Domain.ComplexRequests
{
    public class UpdateRequest
    {
        public LoginDto currentUser { get; set; } 
        public UpdateUserDto updateModel { get; set; }
    }
}
