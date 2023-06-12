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

        [HttpGet]
        [Route("api/v1/email/confirmar-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmarEmail([FromQuery]string codigo)
        {
            try
            {
                var email = await _service.ConfirmarEmailAsync(codigo);

                return Ok(email);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



       
    }
}
