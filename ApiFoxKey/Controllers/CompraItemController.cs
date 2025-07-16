/*
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
    public class CompraItemController : ControllerBase
    {
        private readonly CompraItemService _service;

        public CompraItemController(CompraItemService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("compraitens")]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetAll();
            return Ok(lista);
        }

        // GET ONE BY ID
        [HttpGet("compraitens/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var entidade = await _service.GetOneById(id);
                if (entidade == null) return NotFound("Registro não encontrado");
                return Ok(entidade);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // CREATE
        [HttpPost("compraitens")]
        public async Task<IActionResult> Create([FromBody] CompraItemDto item)
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
        [HttpPut("compraitens/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CompraItemDto item)
        {
            try
            {
                var atualizado = await _service.Update(id, item);
                if (atualizado == null) return NotFound("Registro não encontrado");
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("compraitens/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletado = await _service.Delete(id);
                if (deletado == null) return NotFound("Registro não encontrado");
                return Ok(deletado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
*/