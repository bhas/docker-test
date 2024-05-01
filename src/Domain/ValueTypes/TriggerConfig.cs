using System.Text.Json;

public class TriggerConfig
{
    public DateTimeOffset? ScheduledDate { get; set; }
    public RecurringTriggerConfig? RecurringSchedule { get; set; }

    public static TriggerConfig? FromJson(string? json)
    {
        return json != null ? JsonSerializer.Deserialize<TriggerConfig>(json) : null;
    }
}

public class RecurringTriggerConfig
{
    public List<string> DayOfWeek { get; set; } = [];
    public TimeSpan? TimeOfDay { get; set; }
}