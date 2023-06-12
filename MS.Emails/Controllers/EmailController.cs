using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.Emails.Entities;
using MS.Emails.Interfaces;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Controllers
{
    [ApiController]
    public class EmailController : Controller
    {
        private readonly ICodigoEmailService _service;

        public EmailController(ICodigoEmailService service)
        {
            _service = service;
        }



        [HttpPost]
        [Route("api/v1/email/gerar-codigo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GerarCodigo(EmailRequestDto request)
        {
            var url_base = string.Format("{0}://{1}", Request.Scheme, Request.Host);

            try
            {
                var codigo = await _service.CadastrarCodigoAsync(request);

                var linkConfirmacao = _service.ObterUrlConfirmacaoAsync(url_base,codigo);

                await _service.EnviarEmailConfirmacaoAsync(request.Email, linkConfirmacao);

                return Ok("Email enviado");
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }

            
        }
    }
}
