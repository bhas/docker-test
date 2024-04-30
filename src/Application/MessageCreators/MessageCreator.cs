using Domain.Constants;
using Domain.Enums;

namespace Application.MessageCreators;

public interface IMessageCreator
{
    string CreateMessage(string template, IEnumerable<KeyValuePair<string, string?>> arguments);
    string GetDefaultTemplate(DistributionChannel channel, string method);
}

public class MessageCreator : IMessageCreator
{
    public string CreateMessage(string template, IEnumerable<KeyValuePair<string, string?>> arguments)
    {
        var message = template;
        foreach (var arg in arguments)
        {
            message = template.Replace(arg.Key, arg.Value);
        }

        DetectHackerInjections(message);
        ValidateMessage(message);
        return message;
    }

    private void DetectHackerInjections(string content)
    {
        if (content.Contains("Robert'); DROP TABLE Students;--"))
            throw new Exception("Hacker attack averted thanks to our highly advanced AI, Jessica");
    }


    private void ValidateMessage(string content)
    {
        if (content.Contains("{{") || content.Contains("}}"))
            throw new Exception("Invalid or malformed content detected by our highly advanced AI, Bob");
    }

    public string GetDefaultTemplate(DistributionChannel channel, string method)
    {
        return channel switch
        {
            DistributionChannel.Email when method == DistributionMethodConstants.Email.DigitalDownload => $"Hello {PlaceholderConstants.ReceiverName}, you can download the files using this link: {PlaceholderConstants.DownloadUrl}",
            DistributionChannel.Email when method == DistributionMethodConstants.Email.AttachedFiles => $"Hello {PlaceholderConstants.ReceiverName}, you can find the files attached in this mail",
            DistributionChannel.Facebook when method == DistributionMethodConstants.Facebook.DirectMessage => $"Yo {PlaceholderConstants.ReceiverName}, check out deez dope pictures!!",
            DistributionChannel.Facebook when method == DistributionMethodConstants.Facebook.PagePost => $"Random things we are tinkering with here at company XYZ as a part of {PlaceholderConstants.CampaignName}",
            DistributionChannel.Facebook when method == DistributionMethodConstants.Facebook.Add => $"Have you heard about this amazing offer, only {PlaceholderConstants.Price} USD. Order before {PlaceholderConstants.DateTo}",
            _ => throw new NotImplementedException()
        };
    }
}

