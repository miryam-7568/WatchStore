using Business;
using Microsoft.EntityFrameworkCore;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUsersServices, UsersServices>();
builder.Services.AddScoped<IUsersData, UsersData>();
builder.Services.AddDbContext<ShopDB_328181300Context>(options =>
    options.UseSqlServer("Data Source=SRV2\\PUPILS;Initial Catalog=ShopDB_328181300;Integrated Security=True; TrustServerCertificate=True"));


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
