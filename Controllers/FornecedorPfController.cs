using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Services;
using ApiLocadora.Dtos;
using ApiLocadora.Models;

namespace ApiLocadora.Controllers
{
    [Route("fornecedorespf")]
    [ApiController]
    public class FornecedorPfController : ControllerBase
    {
        private readonly FornecedorPfService _service;

        public FornecedorPfController(FornecedorPfService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetAll();
            return Ok(lista);
        }

        // GET BY ID
        [HttpGet("{id}")]
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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FornecedorPfDto dto)
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FornecedorPfDto dto)
        {
            try
            {
                var atualizado = await _service.Update(id, dto);
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var excluido = await _service.Delete(id);
                return Ok(excluido);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
