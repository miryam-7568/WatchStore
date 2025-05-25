using DTOs;
using Entities;

namespace Business
{
    public interface IOrdersServices
    {
        Task<List<OrderDto>> GetOrders();
        Task AddOrder(OrderDto orderDto);

    }
}