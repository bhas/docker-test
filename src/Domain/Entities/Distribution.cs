
using Domain.ValueTypes;

namespace Domain.Entities;

public class Distribution
{
    public long Id { get; set; }
    public DateTimeOffset Date { get; set; }
    public long DistributionConfigId { get; set; }
    public DistributionStatus Status { get; set; }
    public string? StatusText { get; set; }
    public ICollection<DistributionAssetLink> Assets { get; set; } = [];
}

public enum DistributionStatus
{
    Unknown = 0,
    Sent = 1,
    Failed = 2
}