using Application.Distribution;
using Infrastructure.Database.Repositories;

namespace DistributionWorker.Workers;

public class ScheduledWorker(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<ScheduledWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope2 = logger.BeginScope(new Dictionary<string, object?> { ["Worker"] = nameof(ScheduledWorker) });
        logger.LogInformation("Starting worker");

        while (!stoppingToken.IsCancellationRequested)
        {
            {
                await ProcessAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        logger.LogInformation("Stopping worker");
    }

    private async Task ProcessAsync(CancellationToken stoppingToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();

        logger.LogInformation("triggered worker");
        var distributionConfigRepository = scope.ServiceProvider.GetRequiredService<IDistributionConfigRepository>();
        var distributionManager = scope.ServiceProvider.GetRequiredService<IDistributionManager>();

        var configs = distributionConfigRepository.GetScheduledConfigs();

        foreach ( var config in configs )
        {
            var triggerConfig = TriggerConfig.FromJson(config.TriggerConfigJson);
            if (triggerConfig == null)
                continue;

            if (triggerConfig.ScheduledDate < DateTimeOffset.UtcNow)
            {
                await distributionManager.ExecuteDistributionAsync(config);
            }
        }
    }
}
