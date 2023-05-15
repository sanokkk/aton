namespace AtonTalent.Domain.Dtos;

public class UpdateUserDto
{
    public string Name { get; init; }

    public int? Gender { get; init; }

    public DateTime? Birthday { get; init; }
}
