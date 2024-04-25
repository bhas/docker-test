using Api.Dtos;
using FastEndpoints;
using static Api.Endpoints.OrderLists.GetOrderListEndpoint;

namespace Api.Endpoints.OrderLists;

public class GetOrderListEndpoint : Endpoint<Request, OrderListDto>
{
    public class Request
    {
        public required string OrderNumber { get; set; }
    }

    public override void Configure()
    {
        Get("/api/order-lists/{orderNumber}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var result = new OrderListDto()
        {
            OrderNumber = req.OrderNumber,
            CustomerName = "Bob Dylan",
            DateTimeOffset = DateTimeOffset.UtcNow,
            Assets = new List<OrderListAssetDto>()
            {
                new OrderListAssetDto
                {
                    AssetId = "ASSET1",
                    Quantity = 1
                },
                new OrderListAssetDto
                {
                    AssetId = "ASSET2",
                    Quantity = 3
                }
            }
        };
        await SendAsync(result);
    }
}
