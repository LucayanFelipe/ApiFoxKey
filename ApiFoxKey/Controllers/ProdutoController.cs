using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("produtos")]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetAll();
            return Ok(lista);
        }

        // GET ONE BY ID
        [HttpGet("produtos/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _service.GetOneById(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // CREATE
        [HttpPost("produtos")]
        public async Task<IActionResult> Create([FromBody] ProdutoDto dto)
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

        // UPDATE
        [HttpPut("produtos/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProdutoDto dto)
        {
            try
            {
                var atualizado = await _service.Update(id, dto);
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("produtos/{id}")]
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
