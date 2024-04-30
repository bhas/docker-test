

using Microsoft.Extensions.Logging;

namespace Application.Integrations;
public class MockFacebookClient(ILogger<MockFacebookClient> logger) : IFacebookClient
{
    public Task PostOnPageAsync(string pageIdentifier, string? text, IEnumerable<string> imgUrls)
    {
        logger.LogInformation("Created a facebook post...");
        return Task.CompletedTask;
    }

    public Task SendDirectMessageAsync(string userId, string message, IEnumerable<string> imgUrls)
    {
        logger.LogInformation("Send a direct message to user...");
        return Task.CompletedTask;
    }
}