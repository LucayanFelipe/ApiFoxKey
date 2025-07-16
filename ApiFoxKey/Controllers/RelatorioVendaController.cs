using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Models;
using ApiLocadora.Services;
using System;
using System.Threading.Tasks;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class RelatorioVendaController : ControllerBase
    {
        private readonly RelatorioVendaService _service;

        public RelatorioVendaController(RelatorioVendaService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("relatoriovenda")]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetAll();
            return Ok(lista);
        }

        // GET ONE BY ID
        [HttpGet("relatoriovenda/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var entidade = await _service.GetOneById(id);
                if (entidade == null) return NotFound("Relatório não encontrado");
                return Ok(entidade);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // CREATE
        [HttpPost("relatoriovenda")]
        public async Task<IActionResult> Create([FromBody] RelatorioVenda item)
        {
            try
            {
                var criado = await _service.Create(item);
                if (criado == null) return BadRequest("Não foi possível criar o relatório.");
                return Ok(criado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // UPDATE
        [HttpPut("relatoriovenda/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RelatorioVenda item)
        {
            try
            {
                var atualizado = await _service.Update(id, item);
                if (atualizado == null) return NotFound("Relatório não encontrado");
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("relatoriovenda/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletado = await _service.Delete(id);
                if (deletado == null) return NotFound("Relatório não encontrado");
                return Ok(deletado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
