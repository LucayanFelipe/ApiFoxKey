using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Models;
using ApiLocadora.Dtos;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class ClientePjController : ControllerBase
    {
        private readonly ClientePjService _service;

        public ClientePjController(ClientePjService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("clientepj")]
        public async Task<IActionResult> Search()
        {
            var lista = await _service.GetAll();
            return Ok(lista);
        }

        // GET ONE BY ID
        [HttpGet("clientepj/{id}")]
        public async Task<IActionResult> SearchId(int id)
        {
            try
            {
                var entidade = await _service.GetOneById(id);
                if (entidade == null) return NotFound("Informação não encontrada");
                return Ok(entidade);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // CREATE
        [HttpPost("clientepj")]
        public async Task<IActionResult> Create([FromBody] ClientePjDto item)
        {
            try
            {
                var criado = await _service.Create(item);
                if (criado == null) return BadRequest("Não foi possível criar o registro.");
                return Ok(criado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // UPDATE
        [HttpPut("clientepj/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientePjDto item)
        {
            try
            {
                var atualizado = await _service.Update(id, item);
                if (atualizado == null) return NotFound("Informação não encontrada");
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("clientepj/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletado = await _service.Delete(id);
                if (deletado == null) return NotFound("Informação não encontrada");
                return Ok(deletado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
