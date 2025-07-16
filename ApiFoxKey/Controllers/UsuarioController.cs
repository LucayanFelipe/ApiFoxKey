using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Models;
using ApiLocadora.Services;
using ApiLocadora.Dtos;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet("usuarios")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _service.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("usuarios/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var usuario = await _service.GetOneById(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("usuarios")]
        public async Task<IActionResult> Create([FromBody] UsuarioDto dto)
        {

            try
            {
                var novo = await _service.Create(dto);
                return Ok(novo);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("usuarios/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioDto dto)
        {
            try
            {
                var atualizada = await _service.Update(id, dto);
                return Ok(atualizada);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("usuarios/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletado = await _service.Delete(id);
                return Ok(deletado);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
