using Domain.Entities;
using FastEndpoints;
using Infrastructure.Database.Repositories;

namespace Api.Endpoints.DistributionConfigs;

public class TriggerConfigEndpoint(IDistributionConfigRepository distributionConfigRepository) : Endpoint<TriggerConfigEndpoint.Request>
{
    public class Request
    {
        public long Id { get; set; }
    }

    public override void Configure()
    {
        Post("/api/distribution-configs/{id}/trigger");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        // should trigger the worker to distribute the data
        await SendOkAsync(cancellation: ct);
    }
}
