using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class OrderRepositoryTest
    {
        [Fact]
        public async Task GetOrders_ReturnsOrders()
        {
            // Arrange
            var order1 = new Order { Id = 1, UserId = 1, OrderDate = DateOnly.FromDateTime(DateTime.Now), OrderSum = 100.0 };
            var order2 = new Order { Id = 2, UserId = 2, OrderDate = DateOnly.FromDateTime(DateTime.Now), OrderSum = 200.0 };
            var orders = new List<Order> { order1, order2 };

            var mockContext = new Mock<ShopDB327742698Context>();
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);

            var ordersData = new OrdersData(mockContext.Object);

            // Act
            var result = await ordersData.GetOrders();

            // Assert
            Assert.Equal(orders, result);
        }

        [Fact]
        public async Task AddOrder_AddsOrderToDatabase()
        {
            // Arrange
            var order = new Order { Id = 1, UserId = 1, OrderDate = DateOnly.FromDateTime(DateTime.Now), OrderSum = 150.0 };

            var mockContext = new Mock<ShopDB327742698Context>();
            var mockOrdersDbSet = new Mock<DbSet<Order>>();
            mockContext.Setup(x => x.Orders).Returns(mockOrdersDbSet.Object);

            var ordersData = new OrdersData(mockContext.Object);

            // Act
            await ordersData.AddOrder(order);

            // Assert
            mockOrdersDbSet.Verify(x => x.AddAsync(It.Is<Order>(o => o == order), It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
