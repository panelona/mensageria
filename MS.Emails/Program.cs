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


builder.Services.AddDbContextFactory<TransientDbContextFactory>(options =>
{
    var connectionString = builder.Configuration["MS_EMAIL_CONNSTRING"];
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
    




var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHostedService<RabbitMqSubscriber>();
builder.Services.AddSingleton<IProcessaEvento, ProcessaEvento>();
builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
builder.Services.AddScoped<ICodigoEmailRepository, CodigoRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICodigoService, CodigoService>();


builder.Services.AddCors(options =>
{
    var corsDomains = builder.Configuration["MS_EMAIL_CORS_DOMAINS"];
    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins(corsDomains)
            .AllowAnyMethod()
            .AllowAnyHeader());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
