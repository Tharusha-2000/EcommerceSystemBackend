using Ecommerce.OrderProcessing.API.Controllers;
using Ecommerce.OrderProcessing.Application.Services;
using Ecommerce.OrderProcessing.Infras;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EcommerceDbContext>();

builder.Services.AddScoped<EcommerceDbContext>();

//---- dependency Injection---------
builder.Services.AddScoped<ICartSer, CartSer>();
builder.Services.AddScoped<IOrderSer, OrderSer>();
builder.Services.AddScoped<IOrderProductSer, OrderProductSer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
