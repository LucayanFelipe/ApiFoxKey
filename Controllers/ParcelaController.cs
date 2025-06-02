using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiLocadora.Controllers
{
    [Route("parcelas")]
    [ApiController]
    public class ParcelaController : ControllerBase
    {
        private readonly ParcelaService _service;

        public ParcelaController(ParcelaService service)
        {
            _service = service;
        }

        // GET: /parcelas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var parcelas = await _service.GetAll();
            return Ok(parcelas);
        }

        // GET: /parcelas/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var parcela = await _service.GetOneById(id);
                return Ok(parcela);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: /parcelas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ParcelaDto item)
        {
            try
            {
                var novaParcela = await _service.Create(item);
                return Ok(novaParcela);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT: /parcelas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ParcelaDto item)
        {
            try
            {
                var atualizada = await _service.Update(id, item);
                return Ok(atualizada);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: /parcelas/{id}
        [HttpDelete("{id}")]
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
