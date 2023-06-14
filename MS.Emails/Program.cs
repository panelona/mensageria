using Microsoft.EntityFrameworkCore;
using MS.Emails.Events;
using MS.Emails.Interfaces;
using MS.Emails.RabbitMq;
using MS.Emails.Respositories;
using MS.Emails.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
                                                                
var connectionString = builder.Configuration.GetValue<string>("MS_EMAIL_CONNSTRING");
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHostedService<RabbitMqSubscriber>();
builder.Services.AddSingleton<IProcessaEvento, ProcessaEvento>();
//builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
builder.Services.AddScoped<ICodigoEmailRepository, CodigoRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICodigoService, CodigoService>();





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
