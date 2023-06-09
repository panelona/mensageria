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
        public IActionResult GerarCodigo(EmailRequestDto request)
        {
            try
            {
                var emailEntity = _service.CadastrarCodigoAsync(request);

                return Ok();
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }

            
        }
    }
}
