using AtonTalent.Domain.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AtonTalent.Domain.ComplexRequests;

public class GetByLoginPassRequest
{
    [Required]
    public LoginDto UserToGet { get; init; }
}
