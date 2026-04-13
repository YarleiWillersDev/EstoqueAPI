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
    public class MovimentacaoController : ControllerBase
    {

        private readonly AppDbContext _context;

        public MovimentacaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<MovimentacaoEstoque>> GetById(long id)
        {
            if (id <= 0)
                return BadRequest("O Id da movimentação de estoque não pode ser menor ou igual a 0.");

            var movimentacao = await _context.MovimentacoesEstoque
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimentacao is null)
                return NotFound("Nenhuma movimentação de estoque foi encontrada para o Id informado.");

            return Ok(movimentacao);
        }

        [HttpGet]
        public async Task<ActionResult<List<MovimentacaoEstoque>>> GetAll()
        {
            return await _context.MovimentacoesEstoque.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<MovimentacaoEstoque>> Create([FromBody] MovimentacaoEstoque movimentacaoEstoque)
        {
            if (movimentacaoEstoque is null)
                throw new ArgumentException("Movimentação de estoque não pode ser null.");

            ValidarMovimentacaoEstoque(movimentacaoEstoque);

            var produto = await ObterProdutoPorIdAsync(movimentacaoEstoque.ProdutoId);

            ValidarEstoqueParaSaida(movimentacaoEstoque, produto);

            EfetuarOperacaoDeEstoque(movimentacaoEstoque, produto);

            _context.MovimentacoesEstoque.Add(movimentacaoEstoque);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = movimentacaoEstoque.Id }, movimentacaoEstoque);
        }

        private void ValidarMovimentacaoEstoque(MovimentacaoEstoque movimentacaoEstoque)
        {
            if (movimentacaoEstoque.Quantidade <= 0)
                throw new ArgumentException("A quantidade de uma movimentação de estoque não pode ser menor ou igual a 0.");

            if (movimentacaoEstoque.TipoMovimentacao != TipoMovimentacao.Entrada
                && movimentacaoEstoque.TipoMovimentacao != TipoMovimentacao.Saida)
                throw new ArgumentException("O tipo de movimentação de estoque é inválido");
        }

        private async Task<Produto> ObterProdutoPorIdAsync(long produtoId)
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == produtoId);

            if (produto is null)
                throw new KeyNotFoundException("Nenhum produto encontrado para o Id informado.");

            return produto;
        }

        private void ValidarEstoqueParaSaida(MovimentacaoEstoque movimentacaoEstoque, Produto produto)
        {
            if (movimentacaoEstoque.TipoMovimentacao == TipoMovimentacao.Saida && produto.Quantidade < movimentacaoEstoque.Quantidade)
                throw new InvalidOperationException("Quantidade insuficiente para operação.");
        }

        private void EfetuarOperacaoDeEstoque(MovimentacaoEstoque movimentacaoEstoque, Produto produto)
        {
            switch (movimentacaoEstoque.TipoMovimentacao)
            {
                case TipoMovimentacao.Entrada:
                    produto.Quantidade += movimentacaoEstoque.Quantidade;
                    break;

                case TipoMovimentacao.Saida:
                    produto.Quantidade -= movimentacaoEstoque.Quantidade;
                    break;

                default:
                    throw new ArgumentException("Tipo de movimentação inválido.");
            }
        }
    }
}