﻿using Microsoft.AspNetCore.Mvc;
using MS.Pedidos.Entities;
using MS.Pedidos.Interfaces.Service;
using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Controllers
{
    [ApiController]
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        [Route("api/v1/pedido")]
        public async Task<int> PostAsync(PedidoDTO PedidoDto)
        {
            int NumeroPedido = await _pedidoService.AddAsync(PedidoDto);
            return NumeroPedido;
            
        }

        [HttpPatch]
        [Route("api/v1/pedido")]
        public async Task<IActionResult> PacthStatusPedido(StatusPedido statusAtualizado, Guid idPedido)
        {
            try
            {
                var status = _pedidoService.EditAsync(statusAtualizado, idPedido);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

    }
}