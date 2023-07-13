using MS.Pagamentos.Domain.Interfaces;
using MS.Pagamentos.Domain.Services;
using MS.Pagamentos.Eventos;
using MS.Pagamentos.RabbitMq;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(
        options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<RabbitMqSubscriber>();
builder.Services.AddScoped<IPagamentoService, PagamentoService>();
builder.Services.AddSingleton<IProcessaEvento, ProcessaEvento>();
builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();


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
