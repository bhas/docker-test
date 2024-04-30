
namespace Application.Integrations;

public interface IEmailClient
{
    Task SendEmailAsync(string address, string? topic, string htmlContent, IEnumerable<EmailAttachment> attachments);
}

public class EmailAttachment
{
    public required string FileName { get; set; }
    public required Stream FileStream { get; set; }
}
