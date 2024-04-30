namespace Domain.Entities;

public class ContentDistributionDto()
{
    public DateTimeOffset? DistributionDate { get; set; }
    public string? DistributionChannel { get; set; }
    public string? DistributionMethod { get; set; }
    public IReadOnlyList<ContentDistributionAssetDto> Assets { get; set; } = new List<ContentDistributionAssetDto>();
}

public class ContentDistributionAssetDto
{
    public required string AssetId { get; set; }
    public required string Name { get; set; }
    public required string FileUrl { get; set; }
}
