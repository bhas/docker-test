using Application.Integrations;
using Application.MessageCreators;
using Domain.Constants;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Distribution.Distributors;

public class EmailDistributor(IEmailClient emailClient, ILogger<EmailDistributor> logger, IMessageCreator messageCreator) : IDistributor
{
    public bool CanProcess(DistributionChannel channel, string method)
    {
        return channel == DistributionChannel.Email;
    }

    public async Task DistributeAsync(HashSet<string> assetIds, DistributionChannel channel, string method, string? optionsJson)
    {
        logger.LogInformation("Distributing content for {channel} {method}", channel, method);
        var options = EmailDistributorOptions.Parse(optionsJson);


        var downloadUrl = method == DistributionMethodConstants.Email.DigitalDownload ? "Some url" : null;


        var template = options.Template ?? messageCreator.GetDefaultTemplate(channel, method);
        var arguments = new[]
        {
            KeyValuePair.Create(PlaceholderConstants.DownloadUrl, downloadUrl),
            KeyValuePair.Create(PlaceholderConstants.ReceiverName, options.FullName),
        };
        var content = messageCreator.CreateMessage(template, arguments);
        var attachments = new List<EmailAttachment>();
        if (method == DistributionMethodConstants.Email.AttachedFiles)
        {
            attachments.Add(new EmailAttachment { FileName = "Files.zip", FileStream = File.OpenRead("Somefile") });
        }


        await emailClient.SendEmailAsync(options.EmailAddress, options.Topic, content, attachments);
        logger.LogInformation("Completed distribution of content for {channel} {method}", channel, method);
    }
}

public class EmailDistributorOptions
{
    public required string EmailAddress { get; set; }

    public string? FullName { get; set; }

    public string? Topic { get; set; }

    /// <summary>
    /// A template used for the content of the email. A default message will be used if no template is provided. 
    /// </summary>
    public string? Template { get; set; }

    public static EmailDistributorOptions Parse(string? optionsJson)
    {
        if (optionsJson == null)
            throw new ArgumentException($"No options provided");

        var options = JsonSerializer.Deserialize<EmailDistributorOptions>(optionsJson);
        if (options == null)
            throw new ArgumentException($"Could not recognize options");

        return options;
    }
}


