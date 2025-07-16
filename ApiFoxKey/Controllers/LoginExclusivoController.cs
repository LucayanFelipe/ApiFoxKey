using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Models;
using ApiLocadora.Dtos;
using ApiLocadora.Services;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class LoginExclusivoController : ControllerBase
    {
        private readonly LoginExclusivoService _service;

        public LoginExclusivoController(LoginExclusivoService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("loginsexclusivos")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        // GET ONE
        [HttpGet("loginsexclusivos/{id}")]
        public async Task<IActionResult> GetById(int id)
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

        // CREATE
        [HttpPost("loginsexclusivos")]
        public async Task<IActionResult> Create([FromBody] LoginExclusivoDto dto)
        {
            try
            {
                var result = await _service.Create(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // UPDATE
        [HttpPut("loginsexclusivos/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LoginExclusivoDto dto)
        {
            try
            {
                var result = await _service.Update(id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("loginsexclusivos/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
