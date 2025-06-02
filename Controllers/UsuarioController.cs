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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                PerfilAcesso = dto.Perfil_acesso
            };

            try
            {
                var novo = await _service.Create(usuario);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = new Usuario
            {
                Id_usuario = id,
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                PerfilAcesso = dto.Perfil_acesso
            };

            try
            {
                var atualizado = await _service.Update(id, usuario);
                return Ok(atualizado);
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
