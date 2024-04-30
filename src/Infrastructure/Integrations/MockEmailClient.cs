

using Microsoft.Extensions.Logging;

namespace Application.Integrations;
public class MockEmailClient(ILogger<MockEmailClient> logger) : IEmailClient
{
    public Task SendEmailAsync(string address, string? topic, string htmlContent, IEnumerable<EmailAttachment> attachments)
    {
        logger.LogInformation("Send out email...");
        return Task.CompletedTask;
    }
}