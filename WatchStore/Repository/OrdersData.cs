using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrdersData : IOrdersData
    {
        ShopDB327742698Context _ShopDB327742698Context;

        public OrdersData(ShopDB327742698Context shopDB327742698Context)
        {
            _ShopDB327742698Context = shopDB327742698Context;
        }

        public async Task<List<Order>> GetOrders()
        {
            try
            {
                return await _ShopDB327742698Context.Orders.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new CustomApiException(500, "Error reading orders from DB " + ex.Message);
            }
        }

        public async Task AddOrder(Order order)
        {
            try
            {
                await _ShopDB327742698Context.Orders.AddAsync(order);
                await _ShopDB327742698Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new CustomApiException(500, "Error writing order to DB: " + ex.Message);
            }
        }

    }
}
