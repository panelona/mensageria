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


var connectionString = builder.Configuration.GetValue<string>("MS_PEDIDOS_CONNSTRING");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

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
