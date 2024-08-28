namespace Presentation.Shared.Domain.Entities.Common;

public record BaseEntity
{
    public int Id { get; init; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}