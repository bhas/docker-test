namespace Domain.Entities;

public class AddDistributionConfigDto
{
    public DistributionChannel Channel { get; set; }
    public required string Method { get; set; }
    public TriggerType TriggerType { get; set; }
    public TriggerConfig? TriggerConfig { get; set; }
    public AssetSelectorPattern? AssetSelectorPattern { get; set; }
}

