using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using ApiLocadora.Services;
using System;
using System.Threading.Tasks;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class ClientePfController : ControllerBase
    {
        private readonly ClientePfService _service;

        public ClientePfController(ClientePfService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("clientepf")]
        public async Task<IActionResult> Search()
        {
            var list = await _service.GetAll();
            return Ok(list);
        }

        // GET ONE BY ID
        [HttpGet("clientepf/{id}")]
        public async Task<IActionResult> SearchId(int id)
        {
            try
            {
                var item = await _service.GetOneById(id);
                if (item == null) return NotFound("Informação não encontrada");
                return Ok(item);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // CREATE
        [HttpPost("clientepf")]
        public async Task<IActionResult> Create([FromBody] ClientePfDto item)
        {
            try
            {
                var created = await _service.Create(item);
                if (created == null) return NotFound("Informação não encontrada");
                return Ok(created);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // UPDATE
        [HttpPut("clientepf/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientePfDto item)
        {
            try
            {
                var updated = await _service.Update(id, item);
                if (updated == null) return NotFound("Informação não encontrada");
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("clientepf/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.Delete(id);
                if (deleted == null) return NotFound("Informação não encontrada");
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
