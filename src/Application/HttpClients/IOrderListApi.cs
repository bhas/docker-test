using Domain.Entities;

namespace Application.HttpClients;

public interface IOrderListApi
{
    Task<OrderListDto> GetOrderListAsync(string orderNumber);
}
