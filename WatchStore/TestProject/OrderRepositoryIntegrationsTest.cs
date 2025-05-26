using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class OrderRepositoryIntegrationsTest : IClassFixture<DatabaseFixture>
    {
        private readonly ShopDB327742698Context _context;
        private readonly OrdersData _ordersData;

        public OrderRepositoryIntegrationsTest(DatabaseFixture fixture)
        {
            _context = fixture.Context;
            _ordersData = new OrdersData(_context);
        }

        [Fact]
        public async Task GetOrders_ReturnsAllOrders()
        {
            // Arrange
            var user = new User { UserName = "testuser", Password = "password", FirstName = "Test", LastName = "User" };
            var order1 = new Order { User = user, OrderDate = DateOnly.FromDateTime(DateTime.Now), OrderSum = 100.0 };
            var order2 = new Order { User = user, OrderDate = DateOnly.FromDateTime(DateTime.Now), OrderSum = 200.0 };

            _context.Users.Add(user);
            _context.Orders.AddRange(order1, order2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _ordersData.GetOrders();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, o => o.OrderSum == 100.0);
            Assert.Contains(result, o => o.OrderSum == 200.0);
        }

        [Fact]
        public async Task AddOrder_AddsOrderToDatabase()
        {
            // Arrange
            var user = new User { UserName = "newuser", Password = "password", FirstName = "New", LastName = "User" };
            var order = new Order { User = user, OrderDate = DateOnly.FromDateTime(DateTime.Now), OrderSum = 150.0 };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            await _ordersData.AddOrder(order);

            // Assert
            var result = await _context.Orders.Include(o => o.User).FirstOrDefaultAsync(o => o.OrderSum == 150.0);
            Assert.NotNull(result);
            Assert.Equal("newuser", result.User.UserName);
        }
    }
}
