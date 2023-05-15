using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Domain.ComplexRequests
{
    public class DeleteUserRequest
    {
        [Required]
        public string Login { get; init; }

        [Required]
        public DeleteType DeleteType { get; init; }
    }
}
