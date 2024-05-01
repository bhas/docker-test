using Application.HttpClients.Dtos;

namespace Application.HttpClients;

public interface IOrderListApi
{
    Task<OrderListDto> GetOrderListAsync(string orderNumber);
}
