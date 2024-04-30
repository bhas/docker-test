using Application.HttpClients;
using Domain.Entities;
using FastEndpoints;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Endpoints.Assets;

public class TestEndpoint(DockerTestContext _context, IAssetApi _assetApi) : EndpointWithoutRequest<List<AssetDto>>
{

    public override void Configure()
    {
        Get("/api/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        //var result = await _context.Users.ToListAsync();
        var result = await _assetApi.GetAssetsAsync(new HashSet<string>());
        await SendAsync(result.ToList(), cancellation: ct);
    }
}
