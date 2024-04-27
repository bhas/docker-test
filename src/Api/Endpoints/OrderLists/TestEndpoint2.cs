using Domain.Entities;
using FastEndpoints;
using Infrastructure.Database;

namespace Api.Endpoints.OrderLists;

public class TestEndpoint2(DockerTestContext context) : Endpoint<TestEndpoint2.Request>
{
    public class Request
    {
        [FromBody]
        public required User User { get; set; }
    }

    public override void Configure()
    {
        Post("/api/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        context.Users.Add(req.User);
        await context.SaveChangesAsync();
        await SendCreatedAtAsync<TestEndpoint>(new { id = 101 }, "User was created", cancellation: ct);
    }
}
