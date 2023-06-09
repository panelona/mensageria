﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Cadastro.Contracts;
using MS.Cadastro.Interfaces.Services;
using MS.Cadastro.Utils;

namespace MS.Cadastro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioResponse>> GetById(Guid id)
        {
            try
            {
                var result = await _usuarioService.ObterPorIdAsync(id);
                return Ok(result);
            }
            catch (ArgumentException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> Get()
        {
            try
            {
                var result = await _usuarioService.ObterTodosAsync();
                return Ok(result);
            }
            catch (ArgumentException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioResponse>> Post([FromBody] UsuarioRequest usuario)
        {
            try
            {
                var result = await _usuarioService.CriarAsync(usuario);
                return Created(nameof(Post), result);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException); }
        }

        //[HttpPatch]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult> Patch([FromBody] String email)
        //{
        //    try
        //    {
        //        await _usuarioService.AlterarStatusAsync(email);
        //        return Ok();
        //    }
        //    catch (ArgumentException exception)
        //    { 
        //        return BadRequest(exception.Message);
        //    }
        //    catch (Exception ex) 
        //    { 
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException);
        //    }
        //}

        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<UsuarioResponse>> Put([FromBody] UsuarioRequest request, [FromRoute] Guid id)
        {
            try
            {
                var result = await _usuarioService.AtualizarAsync(id, request);
                return Ok(result);
            }
            catch (ArgumentException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _usuarioService.DeletarAsync(id);
                return NoContent();
            }
            catch (ArgumentException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
