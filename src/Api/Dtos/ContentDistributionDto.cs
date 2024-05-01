using Application.HttpClients.Dtos;
using Domain.Entities;

namespace Api.Dtos;

public class ContentDistributionDto
{
    public DateTimeOffset? DistributionDate { get; set; }
    public DistributionChannel DistributionChannel { get; set; }
    public string DistributionMethod { get; set; } = null!;
    public IReadOnlyList<ContentDistributionAssetDto> Assets { get; set; } = new List<ContentDistributionAssetDto>();

    public ContentDistributionDto()
    {
        
    }

    public ContentDistributionDto(Distribution entity, IEnumerable<AssetDto> assets)
    {
        DistributionDate = entity.Date;
        DistributionChannel = entity.Channel;
        DistributionMethod = entity.Method;
        Assets = assets.Select(x => new ContentDistributionAssetDto { 
            AssetId = x.AssetId,
            Name = x.Name,
            FileUrl = x.Path
        }).ToList();
    }
}

public class ContentDistributionAssetDto
{
    public required string AssetId { get; set; }
    public required string Name { get; set; }
    public required string FileUrl { get; set; }
}
