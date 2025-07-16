
/*
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class ItemVendaController : ControllerBase
    {
        private readonly ItemVendaService _service;

        public ItemVendaController(ItemVendaService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("itens-venda")]
        public async Task<IActionResult> GetAll()
        {
            var itens = await _service.GetAll();
            return Ok(itens);
        }

        // GET ONE BY ID
        [HttpGet("itens-venda/{id}")]
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
        [HttpPost("itens-venda")]
        public async Task<IActionResult> Create([FromBody] ItemVendaDto itemDto)
        {
            try
            {
                var novo = await _service.Create(itemDto);
                return Ok(novo);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // UPDATE
        [HttpPut("itens-venda/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemVendaDto itemDto)
        {
            try
            {
                var atualizado = await _service.Update(id, itemDto);
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("itens-venda/{id}")]
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
*/