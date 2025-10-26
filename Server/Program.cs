using BasedLibrary.DTOs.JWTSection;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Authentication;
using ServerLibrary.Constants;
using ServerLibrary.Data;

var builder = WebApplication.CreateBuilder(args);

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database Register
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(ServerConstant.DefaultConnection) ?? throw new InvalidOperationException("Sorry, Your Connection is not found"));
});

// Configer JWTSection
builder.Services.Configure<JWTSection>(builder.Configuration.GetSection(ServerConstant.JWTSection));

// Authentication Services
builder.Services.AddScoped<TokenService>();



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
