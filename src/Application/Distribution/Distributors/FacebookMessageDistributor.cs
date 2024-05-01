using Application.Integrations;
using Application.Utilities;
using Domain.Constants;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Distribution.Distributors;

public class FacebookMessageDistributor(IFacebookClient facebookClient, ILogger<FacebookMessageDistributor> logger, IMessageCreator messageCreator) : IDistributor
{
    public bool CanProcess(DistributionChannel channel, string method)
    {
        return channel == DistributionChannel.Facebook && method == DistributionMethodConstants.Facebook.DirectMessage;
    }

    public async Task DistributeAsync(HashSet<string> assetIds, DistributionChannel channel, string method, string? optionsJson)
    {
        logger.LogInformation("Distributing content for {channel} {method}", channel, method);
        var options = FacebookMessageDistributorOptions.Parse(optionsJson);

        var imgUrls = new string[] {
            "Some url1",
        };

        var template = options.Template ?? messageCreator.GetDefaultTemplate(channel, method);
        var arguments = new[]
        {
            KeyValuePair.Create(PlaceholderConstants.ReceiverName, options.CampaignName),
        };
        var message = messageCreator.CreateMessage(template, arguments);
        var attachments = new List<EmailAttachment>();
        if (method == DistributionMethodConstants.Email.AttachedFiles)
        {
            attachments.Add(new EmailAttachment { FileName = "Files.zip", FileStream = File.OpenRead("Somefile") });
        }

        foreach (var userName in options.UserNames)
        {
            await facebookClient.SendDirectMessageAsync(userName, message, imgUrls);
        }
        logger.LogInformation("Completed distribution of content for {channel} {method}", channel, method);
    }
}

public class FacebookMessageDistributorOptions
{
    public required List<string> UserNames { get; set; }
    public string? CampaignName { get; set; }
    public string? Template { get; set; }

    public static FacebookMessageDistributorOptions Parse(string? optionsJson)
    {
        if (optionsJson == null)
            throw new ArgumentException($"No options provided");

        var options = JsonSerializer.Deserialize<FacebookMessageDistributorOptions>(optionsJson);
        if (options == null)
            throw new ArgumentException($"Could not recognize options");

        return options;
    }
}


