using Api.Dtos;
using Domain.Entities;
using FastEndpoints;
using Infrastructure.Database.Repositories;
using System.Text.Json;

namespace Api.Endpoints.Distributions;

public class AddDistributionEndpoint(IDistributionConfigRepository distributionConfigRepository) : Endpoint<AddDistributionEndpoint.Request, DistributionConfigDto>
{
    public class Request
    {
        [FromBody]
        public required AddDistributionConfigDto Config { get; set; }
    }

    public override void Configure()
    {
        Post("/api/distributions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = new DistributionConfig
        {
            Channel = req.Config.Channel,
            Method = req.Config.Method,
            TriggerType = req.Config.TriggerType,
            TriggerConfigJson = req.Config.TriggerConfig != null ? JsonSerializer.Serialize(req.Config.TriggerConfig) : null,
            AssetSelectorPatternJson = req.Config.AssetSelectorPattern != null ? JsonSerializer.Serialize(req.Config.AssetSelectorPattern) : null,
        };
        distributionConfigRepository.Add(entity);

        var dto = new DistributionConfigDto(entity);
        await SendAsync(dto, cancellation: ct);
    }
}
