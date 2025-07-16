using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class NotaFiscalController : ControllerBase
    {
        private readonly NotaFiscalService _service;

        public NotaFiscalController(NotaFiscalService service)
        {
            _service = service;
        }

        [HttpGet("notafiscal")]
        public async Task<ActionResult<ICollection<NotaFiscal>>> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpGet("notafiscal/{id}")]
        public async Task<ActionResult<NotaFiscal>> GetOneById(int id)
        {
            try
            {
                var result = await _service.GetOneById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("notafiscal")]
        public async Task<ActionResult<NotaFiscal>> Create([FromBody] NotaFiscalDto dto)
        {
            try
            {
                var result = await _service.Create(dto);
                return CreatedAtAction(nameof(GetOneById), new { id = result.Id_nota_fiscal }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("notafiscal/{id}")]
        public async Task<ActionResult<NotaFiscal>> Update(int id, [FromBody] NotaFiscalDto dto)
        {
            try
            {
                var result = await _service.Update(id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("notafiscal/{id}")]
        public async Task<ActionResult<NotaFiscal>> Delete(int id)
        {
            try
            {
                var result = await _service.Delete(id);
                if (result == null)
                    return NotFound("Nota Fiscal não encontrada.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
