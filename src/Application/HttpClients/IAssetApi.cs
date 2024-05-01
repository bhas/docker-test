using Domain.Entities;

namespace Application.HttpClients;

public interface IAssetApi
{
    Task<IReadOnlyList<AssetDto>> GetAssetsAsync(HashSet<string> assetIds, HashSet<string> folderIds);
}
