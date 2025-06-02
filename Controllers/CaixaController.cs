using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Models;
using ApiLocadora.Services;
using System;
using System.Threading.Tasks;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class CaixaController : ControllerBase
    {
        private readonly CaixaService _service;

        public CaixaController(CaixaService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("caixas")]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetAll();
            return Ok(lista);
        }

        // GET ONE BY ID
        [HttpGet("caixas/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var caixa = await _service.GetOneById(id);
                if (caixa == null) return NotFound("Caixa não encontrado");
                return Ok(caixa);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // CREATE
        [HttpPost("caixas")]
        public async Task<IActionResult> Create([FromBody] Caixa item)
        {
            try
            {
                var criado = await _service.Create(item);
                if (criado == null) return BadRequest("Não foi possível criar o caixa.");
                return Ok(criado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // UPDATE
        [HttpPut("caixas/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Caixa item)
        {
            try
            {
                var atualizado = await _service.Update(id, item);
                if (atualizado == null) return NotFound("Caixa não encontrado");
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("caixas/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletado = await _service.Delete(id);
                if (deletado == null) return NotFound("Caixa não encontrado");
                return Ok(deletado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
