using Microsoft.EntityFrameworkCore;
using MS.Cadastro.Events;
using MS.Cadastro.Interfaces.Repositories;
using MS.Cadastro.Interfaces.Services;
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
builder.Services.AddHostedService<RabbitMqSubscriberCadastro>();
#endregion

#region Context
builder.Services.AddDbContextFactory<TransientDbContextFactory>(options =>
{
    var connectionString = builder.Configuration["MS_CADASTRO_CONNSTRING"];
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
#endregion

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
