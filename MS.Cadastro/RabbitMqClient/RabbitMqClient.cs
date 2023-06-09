using MS.Cadastro.Contracts;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MS.Cadastro.RabbitMqClient
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly IConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly IModel _channel;

        public RabbitMqClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMqHost"],
                Port = Int32.Parse(_configuration["RabbitMqPort"]), UserName = _configuration["RabbitMqUser"],
                Password = _configuration["RabbitMqPassword"], VirtualHost = _configuration["RabbitMqVhost"] }.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        }

        public void EnviaParaMsEmail(UsuarioResponse usuarioResponse)
        {
            string msg = JsonSerializer.Serialize(usuarioResponse);
            var body = Encoding.UTF8.GetBytes(msg);

            _channel.BasicPublish(exchange: "trigger",
                routingKey: "",
                basicProperties: null,
                body: body
                );
        }
    }
}
