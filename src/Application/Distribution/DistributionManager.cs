
using Application.Distribution.Distributors;
using Application.HttpClients;
using Domain.Entities;
using Microsoft.Extensions.Logging;
namespace Application.Distribution;

public interface IDistributionManager
{
    Task ExecuteDistributionAsync(DistributionConfig config);
}

public class DistributionManager(IAssetApi assetApi, ILogger<DistributionManager> logger, IEnumerable<IDistributor> distributors) : IDistributionManager
{
    public async Task ExecuteDistributionAsync(DistributionConfig config)
    {
        var assets = await GetAssets(config);
        var assetIds = assets.Select(x => x.AssetId).ToHashSet();
        var distributor = FindDistributor(config.Channel, config.Method);
        await distributor.DistributeAsync(assetIds, config.Channel, config.Method, null);
        logger.LogInformation("Distributed {count} assets via {channel} {method}. configId = {configId}", assets.Count(), config.Channel, config.Method, config.Id);
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
