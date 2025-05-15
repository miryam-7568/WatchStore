using Entities;

namespace Repository
{
    public interface IOrdersData
    {
        Task<List<Order>> GetOrders();

        Task AddOrder(Order order);
    }
}