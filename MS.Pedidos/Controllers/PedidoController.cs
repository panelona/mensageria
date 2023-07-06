using Microsoft.AspNetCore.Mvc;
using MS.Pedidos.Interfaces.Service;
using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }
        
        public async Task<int> PostAsync(PedidoDTO PedidoDto)
        {
            int NumeroPedido = await _pedidoService.AddAsync(PedidoDto);
            return NumeroPedido;
            
        }
    }
}
