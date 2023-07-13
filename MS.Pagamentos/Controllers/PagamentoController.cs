using Microsoft.AspNetCore.Mvc;
using MS.Pagamentos.Domain.Entities;
using MS.Pagamentos.Domain.Interfaces;

namespace MS.Pagamentos.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoService _service;
    
        public PagamentoController(IPagamentoService pagamentoService)
        {
            _service = pagamentoService;
        }
    
        //[HttpPost]
        //[ProducesResponseType(201)]
        //public virtual async Task<ActionResult> PostAsync([FromBody] DadosPagamentoEntitie request)
        //{
        //    RespostaPagamentoEntitie response = await _service.AdicionarPagamentoAsync(request);
    
        //    return Ok(response);
        //}
    }
   
}
