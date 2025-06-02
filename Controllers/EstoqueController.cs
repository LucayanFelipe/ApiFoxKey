using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Services;
using ApiLocadora.Dtos;
using ApiLocadora.Models;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly EstoqueService _service;

        public EstoqueController(EstoqueService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("estoques")]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetAll();
            return Ok(lista);
        }

        // GET ONE BY ID
        [HttpGet("estoques/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _service.GetOneById(id);
                if (item == null) return NotFound("Estoque não encontrado.");
                return Ok(item);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // CREATE
        [HttpPost("estoques")]
        public async Task<IActionResult> Create([FromBody] EstoqueDto dto)
        {
            try
            {
                var criado = await _service.Create(dto);
                return Ok(criado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // UPDATE
        [HttpPut("estoques/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EstoqueDto dto)
        {
            try
            {
                var atualizado = await _service.Update(id, dto);
                if (atualizado == null) return NotFound("Estoque não encontrado para atualizar.");
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("estoques/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletado = await _service.Delete(id);
                if (deletado == null) return NotFound("Estoque não encontrado para deletar.");
                return Ok(deletado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
