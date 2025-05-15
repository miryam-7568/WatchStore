using Entities;

namespace Business
{
    public interface IOrdersServices
    {
        Task<List<Order>> GetOrders();
        Task AddOrder(Order order);

    }
}