
namespace Domain.Entities;

public class AssetSearchResultDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public long TotalCount { get; set; }
    public IReadOnlyList<AssetDto> Items { get; set; } = new List<AssetDto>();
}
