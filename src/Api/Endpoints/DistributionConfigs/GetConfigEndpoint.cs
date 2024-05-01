using Api.Dtos;
using FastEndpoints;
using Infrastructure.Database.Repositories;

namespace Api.Endpoints.DistributionConfigs;

public class GetConfigEndpoint(IDistributionConfigRepository distributionConfigRepository) : Endpoint<GetConfigEndpoint.Request, DistributionConfigDto>
{
    public class Request
    {
        public long Id { get; set; }
    }

    public override void Configure()
    {
        Get("/api/distribution-configs/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = distributionConfigRepository.Get(req.Id);
        var dto = new DistributionConfigDto(entity);
        await SendAsync(dto, cancellation: ct);
    }
}
