using System.Text;
using System.Text.Json;
using MS.Emails.Interfaces;
using MS.Emails.Respositories.Dto;
using RabbitMQ.Client;

namespace MS.Emails.RabbitMq
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
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
            _channel.ExchangeDeclare(exchange:"direct",type: ExchangeType.Direct);
            

        }
        public void EnviaEmailConfirmado(string email)
        {
            string mensagem = JsonSerializer.Serialize(email);
            var body = Encoding.UTF8.GetBytes(mensagem);

            _channel.BasicPublish(exchange: "direct",
                routingKey: "usuarioAtivo",
                basicProperties: null,
                body: body
            );
        }
    }

    
}
