using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class OrdersServices : IOrdersServices
    {
        private readonly IOrdersData _ordersData;

        public OrdersServices(IOrdersData ordersData)
        {
            _ordersData = ordersData;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _ordersData.GetOrders();
        }
        public async Task AddOrder(Order order)
        {
             await _ordersData.AddOrder(order);
        }

    }
}
