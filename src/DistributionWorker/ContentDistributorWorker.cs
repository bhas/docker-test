namespace DistributionWorker;

public class ContentDistributorWorker : BackgroundService
{
    private readonly ILogger<ContentDistributorWorker> _logger;

    public ContentDistributorWorker(ILogger<ContentDistributorWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope2 = _logger.BeginScope(new Dictionary<string, object?> { ["Worker"] = nameof(ContentDistributorWorker) });
        _logger.LogInformation("Starting worker");

        while (!stoppingToken.IsCancellationRequested)
        {
            using (_logger.BeginScope(new Dictionary<string, object?> { ["ExecutionId"] = Guid.NewGuid().ToString() }))
            {
                _logger.LogInformation("Running job");
                await ProcessAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }

        _logger.LogInformation("Stopping worker");
    }

    private async Task ProcessAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Called worker");
    }
}
