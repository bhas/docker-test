namespace Domain.Entities;

public class OrderListDto()
{
    public required string OrderNumber { get; set; }
    public required string CustomerName { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public int TotalAssets { get; set; }
    public IReadOnlyList<OrderListAssetDto> Assets { get; set; } = new List<OrderListAssetDto>();
}

public class OrderListAssetDto
{
    public required string AssetId { get; set; }
    public required int Quantity { get; set; }
}
