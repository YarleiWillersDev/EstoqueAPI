using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetById(long id)
        {
            if (id <= 0)
                return BadRequest("O ID do produto não pode ser menor ou igual a 0.");

            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

            if (produto is null)
                return NotFound("Nenhum produto com este ID foi encontrado.");

            return Ok(produto);
        }

        [HttpGet("categoria/{id}")]
        public async Task<ActionResult<List<Produto>>> GetByCategoriaId(long id)
        {
            if (id <= 0)
                return BadRequest("O ID da categorai não pode ser menor ou igual a 0.");

            var produtos = await _context.Produtos
                .Where(p => p.CategoriaId == id)
                .ToListAsync();

            if (!produtos.Any())
                return NotFound("Nenhum produto com este ID de categoria foi encontrado.");

            return Ok(produtos);
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> GetAll()
        {
            var produtos = await _context.Produtos.ToListAsync();

            return Ok(produtos);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Create([FromBody] Produto produto)
        {
            if (produto is null)
                return BadRequest("O produto não pode ser null");

            var validationResult = ValidarProduto(produto);

            if (validationResult is not OkResult)
                return validationResult;

            var categoriaExiste = await _context.Categorias
                .AnyAsync(c => c.Id == produto.CategoriaId);

            if (!categoriaExiste)
                return BadRequest("Categoria informada não existe.");

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        private ActionResult ValidarProduto(Produto produto)
        {
            if (produto.Nome is null)
                return BadRequest("O nome do produto não pode ser nulo.");

            if (produto.CategoriaId <= 0)
                return BadRequest("O ID da categoria não pode ser menor ou igual a 0.");

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            if (id <= 0)
                return BadRequest("O ID do produto não pode ser menor ou igual a 0.");

            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

            if (produto is null)
                return NotFound("Nenhum produto com este ID foi encontrado.");

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}