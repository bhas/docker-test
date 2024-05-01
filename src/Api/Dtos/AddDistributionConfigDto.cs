namespace Domain.Entities;

public class AddDistributionConfigDto
{
    public DistributionChannel Channel { get; set; }
    public required string Method { get; set; }
    public TriggerType TriggerType { get; set; }
    public TriggerConfigDto? TriggerConfig { get; set; }
    public AssetSelectorPatternDto? AssetSelectorPattern { get; set; }
}

public class AssetSelectorPatternDto
{
    public List<string> FolderIds { get; set; } = [];
    public List<string> AssetIds { get; set; } = [];
}

public class TriggerConfigDto
{
    public DateTimeOffset? ScheduledDate { get; set; }
    public RecurringTriggerConfigDto? RecurringSchedule { get; set; }
}

public class RecurringTriggerConfigDto
{
    public List<string> DayOfWeek { get; set; } = [];
    public TimeSpan? TimeOfDay { get; set; }
}