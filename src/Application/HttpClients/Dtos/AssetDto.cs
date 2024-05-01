namespace Application.HttpClients.Dtos;

public class AssetDto
{
    public required string AssetId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? FileFormat { get; set; }
    public long FileSize { get; set; }
    public required string Path { get; set; }
}
