using Business;
using Microsoft.EntityFrameworkCore;
using Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUsersServices, UsersServices>();
builder.Services.AddScoped<IUsersData, UsersData>();
builder.Services.AddDbContext<ShopDB327742698Context>(options=>options.UseSqlServer ("Data Source = srv2\\pupils; Initial Catalog = ShopDB327742698; Integrated Security = True; TrustServerCertificate=True"));


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
