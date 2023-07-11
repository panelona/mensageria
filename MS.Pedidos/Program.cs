using Microsoft.EntityFrameworkCore;
using MS.Pedidos.Events;
using MS.Pedidos.Interfaces;
using MS.Pedidos.Interfaces.Repository;
using MS.Pedidos.Interfaces.Service;
using MS.Pedidos.RabbitMq;
using MS.Pedidos.Repository;
using MS.Pedidos.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHostedService<RabbitMqSubscriber>();
builder.Services.AddSingleton<IProcessaEvento, ProcessaEvento>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
var connectionString = builder.Configuration.GetValue<string>("MS_PEDIDOS_CONNSTRING");
builder.Services.AddDbContextFactory<TransientDbContextFactory>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//builder.Services.AddCors(options =>
//{
//    var corsDomains = builder.Configuration["MS_PEDIDOS_CORS_DOMAINS"];
//    options.AddPolicy("CorsPolicy",
//        builder => builder
//            .WithOrigins(corsDomains)
//            .AllowAnyMethod()
//            .AllowAnyHeader());
//});
//app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
