using AutoMapper;
using DTOs;
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
        private readonly IMapper _mapper;

        public OrdersServices(IOrdersData ordersData, IMapper mapper)
        {
            _ordersData = ordersData;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> GetOrders()
        {
            List<Order> orders = await _ordersData.GetOrders();
            return _mapper.Map<List<OrderDto>>(orders);
        }
        public async Task AddOrder(OrderDto orderDto)
        {
            Order order = _mapper.Map<Order>(orderDto);
             await _ordersData.AddOrder(order);
        }

    }
}
