using AtonTalent.Domain.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AtonTalent.Domain.ComplexRequests;

public class GetByLoginRequest
{
    [Required]
    public LoginDto CurrentUser { get; set; }

    [Required]
    public string Login { get; set; }
}
