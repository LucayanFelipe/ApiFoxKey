using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Models;
using ApiLocadora.Dtos;
using ApiLocadora.Services;
using System;
using System.Threading.Tasks;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class RelatorioCaixaController : ControllerBase
    {
        private readonly RelatorioCaixaService _service;

        public RelatorioCaixaController(RelatorioCaixaService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("relatoriocaixa")]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetAll();
            return Ok(lista);
        }

        // GET ONE BY ID
        [HttpGet("relatoriocaixa/{id}")]
        public async Task<IActionResult> GetById(int id)
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
        [HttpPost("relatoriocaixa")]
        public async Task<IActionResult> Create([FromBody] RelatorioCaixaDto item)
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
        [HttpPut("relatoriocaixa/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RelatorioCaixaDto item)
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
        [HttpDelete("relatoriocaixa/{id}")]
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
