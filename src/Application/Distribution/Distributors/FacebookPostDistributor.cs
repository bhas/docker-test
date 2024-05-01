using Application.Integrations;
using Application.Utilities;
using Domain.Constants;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Distribution.Distributors;

public class FacebookPostDistributor(IFacebookClient facebookClient, ILogger<FacebookPostDistributor> logger, IMessageCreator messageCreator) : IDistributor
{
    public bool CanProcess(DistributionChannel channel, string method)
    {
        return channel == DistributionChannel.Facebook && method == DistributionMethodConstants.Facebook.PagePost;
    }

    public async Task DistributeAsync(HashSet<string> assetIds, DistributionChannel channel, string method, string? optionsJson)
    {
        logger.LogInformation("Distributing content for {channel} {method}", channel, method);
        var options = FacebookPostDistributorOptions.Parse(optionsJson);

        var imgUrls = new string[] {
            "Some url"
        };

        var template = options.Template ?? messageCreator.GetDefaultTemplate(channel, method);
        var arguments = new[]
        {
            KeyValuePair.Create(PlaceholderConstants.CampaignName, options.CampaignName),
        };
        var content = messageCreator.CreateMessage(template, arguments);
        var attachments = new List<EmailAttachment>();
        if (method == DistributionMethodConstants.Email.AttachedFiles)
        {
            attachments.Add(new EmailAttachment { FileName = "Files.zip", FileStream = File.OpenRead("Somefile") });
        }

        foreach (var pageIdentifier in options.PageIdentifiers)
        {
            await facebookClient.PostOnPageAsync(pageIdentifier, content, imgUrls);
        }
        logger.LogInformation("Completed distribution of content for {channel} {method}", channel, method);
    }
}

public class FacebookPostDistributorOptions
{
    public required List<string> PageIdentifiers { get; set; }
    public string? CampaignName { get; set; }
    public string? Template { get; set; }

    public static FacebookPostDistributorOptions Parse(string? optionsJson)
    {
        if (optionsJson == null)
            throw new ArgumentException($"No options provided");

        var options = JsonSerializer.Deserialize<FacebookPostDistributorOptions>(optionsJson);
        if (options == null)
            throw new ArgumentException($"Could not recognize options");

        return options;
    }
}


