
namespace Domain.ValueTypes;
public class DistributionAssetLink
{
    public required string AssetId { get; set; }
    public long DistributionId { get; set; }
}