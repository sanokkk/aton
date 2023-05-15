using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Domain.ComplexRequests
{
    public class DeleteUserRequest
    {
        public LoginDto CurrentUser { get; init; }

        public string Login { get; init; }

        public DeleteType DeleteType { get; init; }
    }
}
