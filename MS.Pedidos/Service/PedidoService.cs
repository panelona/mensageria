using AutoMapper;
using MS.Pedidos.Entities;
using MS.Pedidos.Interfaces;
using MS.Pedidos.Interfaces.Repository;
using MS.Pedidos.Interfaces.Service;
using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private IMapper _mapper;
        private IRabbitMqClient _rabbitMqClient;

        public PedidoService(IPedidoRepository repository, IMapper mapper, IRabbitMqClient rabbitMqClient)
        {
            _rabbitMqClient = rabbitMqClient;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(PedidoDTO pedido)
        {
            var entidadeMapeada = _mapper.Map<Pedido>(pedido);
            entidadeMapeada.NumeroPedido += 1;
            entidadeMapeada.StatusPedido = StatusPedido.AGUARDANDO;
            entidadeMapeada.Id = Guid.NewGuid();
            
            await _repository.Post(entidadeMapeada);
            var pagamento = _mapper.Map<PedidoPagamento>(pedido);
            var envio = _mapper.Map<PedidoEnvio>(pedido);
            _rabbitMqClient.EnviaParaPagamento(pagamento);
            _rabbitMqClient.EnviaParaEnvios(envio);

            return entidadeMapeada.NumeroPedido;
        }

        public async Task EditAsync(PedidoAtualizaStatusDTO atualizaStatus)
        {
            var pedidoEncontrado = await _repository.GetByEmail(atualizaStatus.EmailCliente);
            pedidoEncontrado.StatusPedido = atualizaStatus.statusNovo;
            await _repository.Patch(pedidoEncontrado);
            var pagamento = _mapper.Map<PedidoPagamento>(atualizaStatus);
            var envio = _mapper.Map<PedidoEnvio>(atualizaStatus);
            _rabbitMqClient.EnviaParaPagamento(pagamento);
            _rabbitMqClient.EnviaParaEnvios(envio);
        }
        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }      

        public Task EditItemAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PedidoDTO>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDTO> ReadPedidoAsync()
        {
            throw new NotImplementedException();
        }
        
    }
}
