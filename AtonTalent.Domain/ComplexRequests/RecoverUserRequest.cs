using AtonTalent.Domain.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AtonTalent.Domain.ComplexRequests;

public class RecoverUserRequest
{
    [Required]
    public LoginDto CurrentUser { get; init; }

    [Required]
    public Guid Id { get; init; }
}
