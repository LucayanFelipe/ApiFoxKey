using Microsoft.AspNetCore.Mvc;
using ApiLocadora.Services;
using ApiLocadora.Models;
using ApiLocadora.Dtos;

namespace ApiLocadora.Controllers
{
    [Route("/")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet("categorias")]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _service.GetAll();
            return Ok(categorias);
        }

        // GET ONE BY ID
        [HttpGet("categorias/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var categoria = await _service.GetOneById(id);
                if (categoria == null) return NotFound("Categoria não encontrada.");
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // CREATE
        [HttpPost("categorias")]
        public async Task<IActionResult> Create([FromBody] CategoriaDto dto)
        {
            try
            {
                var novaCategoria = await _service.Create(dto);
                return Ok(novaCategoria);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // UPDATE
        [HttpPut("categorias/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoriaDto dto)
        {
            try
            {
                var categoriaAtualizada = await _service.Update(id, dto);
                if (categoriaAtualizada == null) return NotFound("Categoria não encontrada.");
                return Ok(categoriaAtualizada);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE
        [HttpDelete("categorias/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var categoriaExcluida = await _service.Delete(id);
                if (categoriaExcluida == null) return NotFound("Categoria não encontrada.");
                return Ok(categoriaExcluida);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
