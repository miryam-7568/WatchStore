using Microsoft.EntityFrameworkCore;
using Repository;

namespace TestProject
{
    public class DatabaseFixture : IDisposable
    {
        public ShopDB327742698Context Context { get; private set; }

        public DatabaseFixture()
        {
            // Set up the test database connection and initialize the context
            var options = new DbContextOptionsBuilder<ShopDB327742698Context>()
                .UseSqlServer("Server=srv2\\pupils;Database=Tests327742698;Trusted_Connection=True;Integrated Security=True; TrustServerCertificate=True;")
                .Options;
            Context = new ShopDB327742698Context(options);
            Context.Database.EnsureCreated();// create the data base
        }

        public void Dispose()
        {
            // Clean up the test database after all tests are completed
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}