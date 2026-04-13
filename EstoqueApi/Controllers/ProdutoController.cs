using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.Model;
using EstoqueApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {

        private readonly IProdutoService _service;

        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetById(long id)
        {
            if (id <= 0)
                return BadRequest("O ID do produto não pode ser menor ou igual a 0.");

            var produto = await _service.GetByIdAsync(id);

            return Ok(produto);
        }

        [HttpGet("by-categoria/{id}")]
        public async Task<ActionResult<List<Produto>>> GetByCategoriaId(long id)
        {
            if (id <= 0)
                return BadRequest("O ID da categoria não pode ser menor ou igual a 0.");

            var produtos = await _service.GetByCategoriaIdAsync(id);

            return Ok(produtos);
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> GetAll()
        {
            var produtos = await _service.GetAllAsync();

            return Ok(produtos);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Create([FromBody] Produto produto)
        {
            if (produto is null)
                return BadRequest("O produto não pode ser null");

            var novoProduto = await _service.CreateAsync(produto);

            return CreatedAtAction(nameof(GetById), new { id = novoProduto.Id }, novoProduto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            if (id <= 0)
                return BadRequest("O ID do produto não pode ser menor ou igual a 0.");

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}