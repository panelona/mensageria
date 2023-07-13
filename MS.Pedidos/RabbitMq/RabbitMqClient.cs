using MS.Pedidos.Interfaces;
using MS.Pedidos.Repository.DTO;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace MS.Pedidos.RabbitMq
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

        public void EnviaParaEnvios(PedidoEnvio pedido)
        {
            string msg = JsonSerializer.Serialize(pedido);
            var body = Encoding.UTF8.GetBytes(msg);

            _channel.BasicPublish(exchange: "direct",
                routingKey: "envioDebug",
                basicProperties: null,
                body: body
                );
        }

        public void EnviaParaPagamento(PedidoPagamento pedido)
        {
            string msg = JsonSerializer.Serialize(pedido);
            var body = Encoding.UTF8.GetBytes(msg);

            _channel.BasicPublish(exchange: "direct",
                routingKey: "pagamentoDebug",
                basicProperties: null,
                body: body
                );
        }
    }
}
