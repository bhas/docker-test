using Domain.Entities;

namespace Application.Distribution.Distributors;

public interface IDistributor
{
    bool CanProcess(DistributionChannel channel, string method);

    Task DistributeAsync(HashSet<string> assetIds, DistributionChannel channel, string method, string? optionsJson);
}
