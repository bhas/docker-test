using Api.Dtos;
using FastEndpoints;
using Infrastructure.Database.Repositories;

namespace Api.Endpoints.DistributionConfigs;

public class DeleteConfigEndpoint(IDistributionConfigRepository distributionConfigRepository) : Endpoint<DeleteConfigEndpoint.Request, DistributionConfigDto>
{
    public class Request
    {
        public long Id { get; set; }
    }

    public override void Configure()
    {
        Delete("/api/distribution-configs/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        distributionConfigRepository.Delete(req.Id);
        await SendOkAsync(cancellation: ct);
    }
}
