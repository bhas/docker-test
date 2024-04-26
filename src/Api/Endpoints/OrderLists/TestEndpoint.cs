using Domain.Entities;
using FastEndpoints;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Endpoints.OrderLists;

public class TestEndpoint(DockerTestContext _context) : EndpointWithoutRequest<List<User>>
{
    public override void Configure()
    {
        Get("/api/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _context.Users.ToListAsync();
        await SendAsync(result, cancellation: ct);
    }
}
