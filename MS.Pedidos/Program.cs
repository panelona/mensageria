using Microsoft.EntityFrameworkCore;
using MS.Pedidos.Interfaces.Repository;
using MS.Pedidos.Interfaces.Service;
using MS.Pedidos.Repository;
using MS.Pedidos.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
var connectionString = builder.Configuration.GetValue<string>("MS_ENVIO_CONNSTRING");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


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
