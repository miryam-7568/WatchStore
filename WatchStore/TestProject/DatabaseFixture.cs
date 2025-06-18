using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository;
using System;
using System.IO;

namespace TestProject
{
    public class DatabaseFixture : IDisposable
    {
        public ShopDB327742698Context Context { get; private set; }

        public DatabaseFixture()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("TestDatabase");

            var options = new DbContextOptionsBuilder<ShopDB327742698Context>()
                .UseSqlServer(connectionString)
                .Options;

            Context = new ShopDB327742698Context(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
