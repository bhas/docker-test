namespace Api.Dtos;

public class OrderListDto
{
    public required string OrderNumber { get; set; }
    public required string CustomerName { get; set; }
    public DateTimeOffset DateTimeOffset { get; set; }
    public int TotalAssets => Assets.Count;
    public required List<OrderListAssetDto> Assets { get; set; } = new List<OrderListAssetDto>();

    public OrderListDto() { } // empty constructor is added to avoid potential deserialization issues

}

public class OrderListAssetDto()
{
    public required string AssetId { get; set; }
    public int Quantity { get; set; }
}
