using MS.Pedidos.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MS.Pedidos.RabbitMq
{
    public class RabbitMqSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly string _nomeFila;
        private IModel _channel;
        private IProcessaEvento _processaEvento;
        public RabbitMqSubscriber(IConfiguration configuration, IProcessaEvento processaEvento)
        {
            _configuration = configuration;
            _processaEvento = processaEvento;
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
            _nomeFila = _channel.QueueDeclare(_configuration["MS_RABBITMQ_QUEUENAME"]).QueueName;
            _channel.QueueBind(queue: _nomeFila, exchange: "direct", routingKey: "pedidoDebug");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumidor = new EventingBasicConsumer(_channel);
            consumidor.Received += (ch, ea) =>
            {
                var conteudo = Encoding.UTF8.GetString(ea.Body.ToArray());

                _processaEvento.Processa(conteudo);

            };

            _channel.BasicConsume(queue: _nomeFila, autoAck: true, consumer: consumidor);

            return Task.CompletedTask;
        }
    }
}
