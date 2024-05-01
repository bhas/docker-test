using Domain.ValueType;

namespace Domain.Entities;
public class DistributionConfig
{
    public long Id { get; set; }
    public required DistributionChannel Channel { get; set; }
    public required string Method { get; set; }

    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTimeOffset LastModifiedDate { get; set; }
    public bool Deleted { get; set; }

    public TriggerType TriggerType { get; set; }

    /// <summary>
    /// Contains the config for the triggering and may change depending on the TriggerType
    /// Skipped for now
    /// </summary>
    public string? TriggerConfigJson { get; set; }

    /// <summary>
    /// Contains the pattern used for selecting assets, i.e. based on meta data filters such as author, assigned project etc.
    /// Skipped for now
    /// </summary>
    public string? AssetSelectorPatternJson { get; set; }
}

public enum TriggerType
{
    Unknown = 0,

    /// <summary>
    /// Assets will be distributed as soon as they become available
    /// </summary>
    Continously = 1,
    
    /// <summary>
    /// Assets will be distributed on a planned schedule
    /// </summary>
    Scheduled = 2,

    /// <summary>
    /// Asset distribution will be manually triggered by user
    /// </summary>
    Manually = 3,

    /// <summary>
    /// Asset distribution is disabled
    /// </summary>
    None = 4
}

public enum DistributionChannel
{
    Unknown = 0,
    Email = 1,
    Facebook = 2,
    OnlineStore = 3
}
