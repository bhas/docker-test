
using Application.Distribution.Distributors;
using Application.HttpClients;
using Application.HttpClients.Dtos;
using Domain.Constants;
using Domain.Entities;
using Domain.ValueTypes;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text.Json;
namespace Application.Distribution;

public interface IDistributionManager
{
    Task ExecuteDistributionAsync(DistributionConfig config);
}

public class DistributionManager(IAssetApi assetApi, 
    ILogger<DistributionManager> logger, 
    IEnumerable<IDistributor> distributors, 
    IDistributionRepository distributionRepository,
    IDistributionConfigRepository distributionConfigRepository) : IDistributionManager
{
    public async Task ExecuteDistributionAsync(DistributionConfig config)
    {
        var assets = await GetAssets(config);
        var assetIds = assets.Select(x => x.AssetId).ToHashSet();
        var distributor = FindDistributor(config.Channel, config.Method);
        // for now I just hardcode the options but they should be stored in database.
        var optionsJson = GetFakeOptionsJson(config.Channel, config.Method);
        await distributor.DistributeAsync(assetIds, config.Channel, config.Method, optionsJson);
        logger.LogInformation("Distributed {count} assets via {channel} {method}. configId = {configId}", assets.Count(), config.Channel, config.Method, config.Id);

        var distribution = new Domain.Entities.Distribution
        {
            Date = DateTimeOffset.UtcNow,
            DistributionConfigId = config.Id,
            Status = DistributionStatus.Sent,
            Channel = config.Channel,
            Method = config.Method,
            StatusText = null,
            Assets = assetIds.Select(x => new DistributionAssetLink { AssetId = x }).ToList()
        };
        distributionRepository.Add(distribution);
        distributionConfigRepository.Deactivate(config);
    }

    private string? GetFakeOptionsJson(DistributionChannel channel, string method)
    {
        if (channel == DistributionChannel.Email)
        {
            var options = new EmailDistributorOptions
            {
                EmailAddress = "some@email.com",
                FullName = "John Doe",
            };
            return JsonSerializer.Serialize(options);
        }

        if (channel == DistributionChannel.Email)
        {
            if (method == DistributionMethodConstants.Facebook.PagePost)
            {
                var options = new FacebookPostDistributorOptions
                {
                    PageIdentifiers = ["PAGE1"],
                    CampaignName = "Campaign 1"
                };
                return JsonSerializer.Serialize(options);
            }
            if (method == DistributionMethodConstants.Facebook.DirectMessage)
            {
                var options = new FacebookMessageDistributorOptions
                {
                    UserNames = ["Ryan Gosling", "Emma Stone"],
                    CampaignName = "Hollywood"
                };
                return JsonSerializer.Serialize(options);
            }
        }

        return null;
    }

    private async Task<IReadOnlyList<AssetDto>> GetAssets(DistributionConfig config)
    {
        IReadOnlyList<AssetDto> assets = [];
        var assetSelector = AssetSelectorPattern.FromJson(config.AssetSelectorPatternJson);
        if (assetSelector != null)
            assets = await assetApi.GetAssetsAsync(assetSelector.AssetIds, assetSelector.FolderIds);

        if (!assets.Any())
        {
            logger.LogWarning("No assets found. Stopping distribution");
            throw new Exception("No assets found for distribution");
        }

        logger.LogInformation("Found {count} assets", assets.Count());
        return assets;
    }

    private IDistributor FindDistributor(DistributionChannel channel, string method)
    {
        var distributor = distributors.FirstOrDefault(x => x.CanProcess(channel, method));
        if (distributor == null)
        {
            logger.LogError("No distributor found for {channel} and {method}", channel, method);
            throw new Exception("No assets found for distribution");
        }
        return distributor;
    }
}
