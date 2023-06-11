using Microsoft.EntityFrameworkCore;
using MS.Cadastro.EmailService;
using MS.Cadastro.Interfaces.Repositories;
using MS.Cadastro.Interfaces.Services;
using MS.Cadastro.Profiles;
using MS.Cadastro.RabbitMqClient;
using MS.Cadastro.Repositories;
using MS.Cadastro.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region build service e repository
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
#endregion

#region build Rabbit
builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
builder.Services.AddHttpClient<IEmailServiceHttpClient, EmailServiceHttpClient>();
#endregion

#region Variáveis de ambiente
var RabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQHOST");
var RabbitMqPort = Environment.GetEnvironmentVariable("RABBITMQPORT");
var RabbitMqUser = Environment.GetEnvironmentVariable("RABBITMQUSER");
var RabbitMqPassword = Environment.GetEnvironmentVariable("RABBITMQPASSWORD");
var RabbitMqVHost = Environment.GetEnvironmentVariable("RABBITMQVHOST");
var ConnectionString = Environment.GetEnvironmentVariable("MS_CADASTRO_CONNSTRING");
#endregion

#region Context
builder.Services.AddDbContext<UsuarioContext>(opt => opt.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString)));
#endregion

//ConfigureMappers.ConfigureDependenciesMapper(builder.Services);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
