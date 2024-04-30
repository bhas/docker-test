
namespace Domain.Entities;

public class BriefingDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? CreatedBy { get; set; }
    public required DateTimeOffset CreatedDate { get; set; }
}
