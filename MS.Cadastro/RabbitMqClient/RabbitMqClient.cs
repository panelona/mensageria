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
                HostName = _configuration["MS_RABBITMQ_HOST"],
                Port = int.Parse(_configuration["MS_RABBITMQ_PORT"]),
                VirtualHost = _configuration["MS_RABBITMQ_VHOST"],
                UserName = _configuration["MS_RABBITMQ_USER"],
                Password = _configuration["MS_RABBITMQ_PASSWORD"]
            }.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "direct", type: ExchangeType.Direct);
        }

        public void EnviaParaMsEmail(UsuarioResponse usuarioResponse)
        {
            string msg = JsonSerializer.Serialize(usuarioResponse);
            var body = Encoding.UTF8.GetBytes(msg);

            _channel.BasicPublish(exchange: "direct",
                routingKey: "email",
                basicProperties: null,
                body: body
                );
        }
    }
}
