using Application.MessageCreators;
using Domain.Constants;
using Domain.Enums;
using FluentAssertions;

namespace Application.UnitTests.Utilities;

public class MessageCreatorTests
{
    private readonly MessageCreator _messageCreator;

    public MessageCreatorTests()
    {
        _messageCreator = new MessageCreator();
    }

    [Theory]
    [InlineData(DistributionChannel.Email, DistributionMethodConstants.Email.AttachedFiles)]
    [InlineData(DistributionChannel.Email, DistributionMethodConstants.Email.DigitalDownload)]
    [InlineData(DistributionChannel.Facebook, DistributionMethodConstants.Facebook.Add)]
    [InlineData(DistributionChannel.Facebook, DistributionMethodConstants.Facebook.DirectMessage)]
    [InlineData(DistributionChannel.Facebook, DistributionMethodConstants.Facebook.PagePost)]
    public void GetDefaultTemplate_ShouldHandleAllChannelsAndMethods(DistributionChannel channel, string method)
    {
        // Arrange

        // Act
        var result = _messageCreator.GetDefaultTemplate(channel, method);

        // Assert
        result.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData(DistributionChannel.Unknown, DistributionMethodConstants.Facebook.Add)]
    [InlineData(DistributionChannel.Email, DistributionMethodConstants.Facebook.Add)]
    [InlineData(DistributionChannel.Facebook, "")]
    public void GetDefaultTemplate_ShouldThrowErrorWithInvalidValue(DistributionChannel channel, string method)
    {
        // Arrange

        // Act
        var action = () => _messageCreator.GetDefaultTemplate(channel, method);

        // Assert
        action.Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void CreateMessage_DetectHackerInjectionsTest()
    {
        // Arrange
        var template = "Some value Robert'); DROP TABLE Students;-- hello";
        var args = new List<KeyValuePair<string, string?>>();

        // Act
        var action = () => _messageCreator.CreateMessage(template, args);

        // Assert
        action.Should().Throw<Exception>();
    }

    [Fact]
    public void CreateMessage_ValidateMessage_ShouldDetectLeftoverPlaceholder()
    {
        // Arrange
        var template = "Hello {{RANDOM_PLACEHOLDER}}";
        var args = new List<KeyValuePair<string, string?>>
        {
            KeyValuePair.Create("{{OTHER_PLACEHOLDER}}", (string?)"value")
        };

        // Act
        var action = () => _messageCreator.CreateMessage(template, args);

        // Assert
        action.Should().Throw<Exception>();
    }

    [Fact]
    public void CreateMessage_ShouldReplacePlaceholders()
    {
        // Arrange
        var template = "Hello {{RECEIVER}}, give me all your {{CURRENCY}}. Thank you {{RECEIVER}}";
        var args = new List<KeyValuePair<string, string?>>
        {
            KeyValuePair.Create("{{RECEIVER}}", (string?)"Mr Fox"),
            KeyValuePair.Create("{{CURRENCY}}", (string?)"USD")
        };

        // Act
        var result = _messageCreator.CreateMessage(template, args);

        // Assert
        result.Should().Be("Hello Mr Fox, give me all your USD. Thank you Mr Fox");
    }
}