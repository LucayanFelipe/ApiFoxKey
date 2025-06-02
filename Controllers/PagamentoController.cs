using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Models;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly PagamentoService _service;

        public PagamentoController(PagamentoService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("pagamentos")]
        public async Task<IActionResult> GetAll()
        {
            var pagamentos = await _service.GetAll();
            return Ok(pagamentos);
        }

        // GET ONE BY ID
        [HttpGet("pagamentos/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var pagamento = await _service.GetOneById(id);
                return Ok(pagamento);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // CREATE
        [HttpPost("pagamentos")]
        public async Task<IActionResult> Create([FromBody] Pagamento pagamento)
        {
            try
            {
                var novoPagamento = await _service.Create(pagamento);
                return CreatedAtAction(nameof(GetById), new { id = novoPagamento.Id_pagamento }, novoPagamento);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // UPDATE
        [HttpPut("pagamentos/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pagamento pagamento)
        {
            try
            {
                var atualizado = await _service.Update(id, pagamento);
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE
        [HttpDelete("pagamentos/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletado = await _service.Delete(id);
                return Ok(deletado);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
