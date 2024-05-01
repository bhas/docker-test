namespace Domain.Entities;

public class DistributionConfigDto
{
    public long Id { get; set; }
    public DistributionChannel Channel { get; set; }
    public string Method { get; set; } = null!;
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTimeOffset LastModifiedDate { get; set; }
    public TriggerType TriggerType { get; set; }
    public dynamic? TriggerConfig { get; set; }
    public dynamic? AssetSelectorPattern { get; set; }

    public DistributionConfigDto()
    {
        
    }

    public DistributionConfigDto(DistributionConfig entity)
    {
        Id = entity.Id;
        Channel = entity.Channel;
        Method = entity.Method;
        CreatedBy = entity.CreatedBy;
        CreatedDate = entity.CreatedDate;
        LastModifiedBy = entity.LastModifiedBy;
        LastModifiedDate = entity.LastModifiedDate;
        TriggerType = entity.TriggerType;
        TriggerConfig = entity.TriggerConfigJson;
        AssetSelectorPattern = entity.AssetSelectorPatternJson;
    }
}
