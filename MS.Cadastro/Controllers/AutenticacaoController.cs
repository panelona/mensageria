using Microsoft.AspNetCore.Mvc;
using MS.Cadastro.Contracts;
using MS.Cadastro.Interfaces.Services;

namespace MS.Cadastro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AutenticacaoResponse>> Login([FromBody] AutenticacaoRequest request)
        {
            try
            {
                var result = await _autenticacaoService.AutenticarAsync(request.Email, request.Senha);
                return Ok(result);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException); }
        }
    }
}
