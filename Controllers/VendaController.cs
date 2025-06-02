using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Models;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly VendaService _service;

        public VendaController(VendaService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("vendas")]
        public async Task<IActionResult> GetAll()
        {
            var vendas = await _service.GetAll();
            return Ok(vendas);
        }

        // GET ONE BY ID
        [HttpGet("vendas/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var venda = await _service.GetOneById(id);
                return Ok(venda);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // CREATE
        [HttpPost("vendas")]
        public async Task<IActionResult> Create([FromBody] Venda venda)
        {
            try
            {
                var nova = await _service.Create(venda);
                return Ok(nova);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // UPDATE
        [HttpPut("vendas/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Venda venda)
        {
            try
            {
                var atualizada = await _service.Update(id, venda);
                return Ok(atualizada);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("vendas/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletada = await _service.Delete(id);
                return Ok(deletada);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
