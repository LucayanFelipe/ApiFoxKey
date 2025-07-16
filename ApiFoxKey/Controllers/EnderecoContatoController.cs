
/*
using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Models;
using ApiLocadora.Dtos;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class EnderecoContatoController : ControllerBase
    {
        private readonly EnderecoContatoService _service;

        public EnderecoContatoController(EnderecoContatoService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("enderecos")]
        public async Task<IActionResult> Search()
        {
            var enderecos = await _service.GetAll();
            return Ok(enderecos);
        }

        // GET ONE BY ID
        [HttpGet("enderecos/{id}")]
        public async Task<IActionResult> SearchId(int id)
        {
            try
            {
                var endereco = await _service.GetOneById(id);
                if (endereco == null) return NotFound("Endereço não encontrado");
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // CREATE
        [HttpPost("enderecos")]
        public async Task<IActionResult> Create([FromBody] EnderecoContatoDto item)
        {
            try
            {
                var endereco = await _service.Create(item);
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // UPDATE
        [HttpPut("enderecos/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EnderecoContatoDto item)
        {
            try
            {
                var endereco = await _service.Update(id, item);
                if (endereco == null) return NotFound("Endereço não encontrado");
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("enderecos/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var endereco = await _service.Delete(id);
                if (endereco == null) return NotFound("Endereço não encontrado");
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
*/