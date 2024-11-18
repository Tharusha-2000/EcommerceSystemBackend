using Ecommerce.ReviewAndRating.Application.Services;
using Ecommerce.ReviewAndRating.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddScoped<IReviewAndRatingService, ReviewAndRatingService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddDbContext<ReviewAndRatingDbContext>(options =>
{
    options.UseSqlServer("Server=tcp:pizzaslicedb.database.windows.net,1433;Initial Catalog=PizzaSliceDB;Encrypt=True;User ID=pizzaSliceDB;Password=EAD123#aa;MultipleActiveResultSets=False;TrustServerCertificate=False;",

   sqlServerOptions =>
   {
       sqlServerOptions.EnableRetryOnFailure();
   }
           );
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:5175", "http://localhost:5176")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowLocalhost");
app.MapControllers();
app.Run();