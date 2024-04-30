
using Domain.Enums;

namespace Domain.ValueType.Channels;

public interface IDistributor
{
    bool CanProcess(DistributionChannel channel, string method);

    Task DistributeAsync(HashSet<string> assetIds, DistributionChannel channel, string method,string? optionsJson);
}
