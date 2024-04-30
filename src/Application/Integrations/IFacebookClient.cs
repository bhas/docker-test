
namespace Application.Integrations;
public interface IFacebookClient
{
    Task PostOnPageAsync(string pageIdentifier, string? text, IEnumerable<string> imgUrls);
    Task SendDirectMessageAsync(string userName, string text, IEnumerable<string> imgUrls);
}