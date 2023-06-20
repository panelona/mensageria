using Microsoft.AspNetCore.Mvc;
using MS.Emails.Interfaces;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Controllers
{
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly ICodigoService _codigoService;

        public EmailController(IEmailService emailService, ICodigoService codigoService)
        {
            _emailService = emailService;
            _codigoService = codigoService;
        }

        [HttpGet]
        [Route("api/v1/email/confirmar-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmarEmail(
            [FromQuery] string codigo)
        {
            try
            {
                var email = await _emailService.ConfirmarEmailAsync(codigo);

                return Ok(email);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("api/v1/email/gerar-codigo-confirmacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GerarCodigoConfirmacao(
            [FromBody] EmailRequestDto request)
        {
            try
            {
                await _emailService.GerarCodigoConfirmacaoAsync(request);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}