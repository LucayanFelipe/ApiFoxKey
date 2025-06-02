using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Models;
using ApiLocadora.Dtos;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly DespesaService _service;

        public DespesaController(DespesaService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("despesas")]
        public async Task<IActionResult> Search()
        {
            var despesas = await _service.GetAll();
            return Ok(despesas);
        }

        // GET ONE BY ID
        [HttpGet("despesas/{id}")]
        public async Task<IActionResult> SearchId(int id)
        {
            try
            {
                var despesa = await _service.GetOneById(id);
                if (despesa == null) return NotFound("Informação não encontrada");
                return Ok(despesa);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // CREATE
        [HttpPost("despesas")]
        public async Task<IActionResult> Create([FromBody] DespesaDto dto)
        {
            try
            {
                var novaDespesa = await _service.Create(dto);
                return Ok(novaDespesa);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // UPDATE
        [HttpPut("despesas/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DespesaDto dto)
        {
            try
            {
                var despesaAtualizada = await _service.Update(id, dto);
                if (despesaAtualizada == null) return NotFound("Informação não encontrada");
                return Ok(despesaAtualizada);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("despesas/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var despesaRemovida = await _service.Delete(id);
                if (despesaRemovida == null) return NotFound("Informação não encontrada");
                return Ok(despesaRemovida);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
