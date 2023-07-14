using System.Text;
using MS.Emails.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MS.Emails.RabbitMq
{
    public class RabbitMqSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly string _queueCadastro;
        private readonly string _queuePedido;
        private readonly string _queuePagamento;
        private readonly string _exchangeName;
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

            _exchangeName = _configuration["MS_RABBITMQ_EXCHANGE"];

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange:_exchangeName,type: _configuration["MS_RABBITMQ_EXCHANGETYPE"]);

            _queueCadastro = _channel.QueueDeclare(_configuration["MS_EMAIL_QUEUE_CADASTRO"]).QueueName;
            _queuePedido = _channel.QueueDeclare(_configuration["MS_EMAIL_QUEUE_PEDIDO"]).QueueName;
            _queuePagamento = _channel.QueueDeclare(_configuration["MS_EMAIL_QUEUE_PAGAMENTO"]).QueueName;


            _channel.QueueBind(queue: _queueCadastro, exchange: _exchangeName, routingKey: "cadastro-email");
            _channel.QueueBind(queue: _queuePedido, exchange:_exchangeName, routingKey: "pedido-email");
            _channel.QueueBind(queue: _queuePagamento, exchange:_exchangeName, routingKey: "pagamento-email");

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
           var consumidor = new EventingBasicConsumer(_channel);
           consumidor.Received += (ch, ea) =>
           {
                var conteudo = Encoding.UTF8.GetString(ea.Body.ToArray());
                var routingKey = ea.RoutingKey;

                

                switch (routingKey)
                {
                   case "cadastro-email": 
                       _processaEvento.EnviaEmailConfirmacao(conteudo);
                       break;
                   case "pedido-email":
                      _processaEvento.EnviaEmailPedidoRealizado(conteudo);
                      break;
                   case "pagamento-email":
                      _processaEvento.EnviaEmailStatusPagamento(conteudo);
                      break;
                   default:
                      Console.WriteLine("A routekey não é valida");
                      break;
                }


           };

           _channel.BasicConsume(queue:_queueCadastro,autoAck:true,consumer:consumidor);

           _channel.BasicConsume(queue: _queuePedido, autoAck: true, consumer: consumidor);

           _channel.BasicConsume(queue: _queuePagamento, autoAck: true, consumer: consumidor);

            return Task.CompletedTask;

        }
    }
}
