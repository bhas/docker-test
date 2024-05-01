using Api.Dtos;
using Application.HttpClients;
using Domain.Entities;
using FastEndpoints;
using Infrastructure.Database.Repositories;
using System.Text.Json;

namespace Api.Endpoints.Distributions;

public class GetDistributionForConfigEndpoint(IDistributionRepository distributionRepository, IAssetApi assetApi) : Endpoint<GetDistributionForConfigEndpoint.Request, List<ContentDistributionDto>>
{
    public class Request
    {
        public long ConfigId { get; set; }
    }

    public override void Configure()
    {
        Get("/api/distribution-configs/{configId}/distributions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var distributions = distributionRepository.GetAllForConfig(req.ConfigId);

        List<ContentDistributionDto> dtos = [];
        foreach(var distribution in distributions)
        {
            var assetIds = distribution.Assets.Select(x => x.AssetId).ToHashSet();
            var assets = await assetApi.GetAssetsAsync(assetIds, []);
            var dto = new ContentDistributionDto(distribution, assets);
            dtos.Add(dto);
        }

        await SendAsync(dtos, cancellation: ct);
    }
}
