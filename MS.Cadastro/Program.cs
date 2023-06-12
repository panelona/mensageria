using Microsoft.EntityFrameworkCore;
using MS.Cadastro.EmailService;
using MS.Cadastro.Events;
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
builder.Services.AddSingleton<IProcessaEventoCadastro, ProcessaEventoCadastro>();
builder.Services.AddHttpClient<IEmailServiceHttpClient, EmailServiceHttpClient>();
builder.Services.AddHostedService<RabbitMqSubscriberCadastro>();
#endregion

#region Variáveis de ambiente
var RabbitMqHost = Environment.GetEnvironmentVariable("MS_RABBITMQ_HOST");
var RabbitMqPort = Environment.GetEnvironmentVariable("MS_RABBITMQ_PORT");
var RabbitMqUser = Environment.GetEnvironmentVariable("MS_RABBITMQ_USER");
var RabbitMqPassword = Environment.GetEnvironmentVariable("MS_RABBITMQ_PASSWORD");
var RabbitMqVHost = Environment.GetEnvironmentVariable("MS_RABBITMQ_VHOST");
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
