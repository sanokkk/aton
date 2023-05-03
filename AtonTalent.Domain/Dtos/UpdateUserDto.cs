using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Domain.Dtos;

public class UpdateUserDto
{
    public string Name { get; init; }

    public int? Gender { get; init; }

    public DateTime? Birthday { get; init; }
}
