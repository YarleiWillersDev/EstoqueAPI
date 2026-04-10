using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.Model;
using EstoqueApi.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetById(long id)
        {
            var categoria = await _service.GetByIdAsync(id);

            return Ok(categoria);
        }

        [HttpGet()]
        public async Task<ActionResult<List<Categoria>>> GetAll()
        {
            var categorias = await _service.GetAllAsync();

            return Ok(categorias);
        }

        [HttpPost()]
        public async Task<ActionResult<Categoria>> Create([FromBody] Categoria categoria)
        {
            if (categoria is null)
                return BadRequest("Categoria não pode ser nula");

            await _service.CreateAsync(categoria);

            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}