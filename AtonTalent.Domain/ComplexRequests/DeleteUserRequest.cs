using AtonTalent.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AtonTalent.Domain.ComplexRequests;

public class DeleteUserRequest
{
    [Required]
    public string Login { get; init; }

    [Required]
    public DeleteType DeleteType { get; init; }
}
