using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.Exceptions;
using EstoqueApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Service
{
    public class MovimentacaoEstoqueService : IMovimentacaoEstoqueService
    {
        private readonly AppDbContext _context;

        public MovimentacaoEstoqueService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MovimentacaoEstoque> Create(MovimentacaoEstoque movimentacaoEstoque)
        {
            if (movimentacaoEstoque is null)
                throw new ValidationException("Movimentação de estoque não pode ser nula.");
            
            ValidarDadosMovimentacaoEstoque(movimentacaoEstoque);

            var produto = await ObterProdutoPorIdAsync(movimentacaoEstoque.ProdutoId);

            ValidarEstoqueParaSaida(movimentacaoEstoque, produto);

            EfetuarOperacaoDeEstoque(movimentacaoEstoque, produto.Quantidade);

            _context.MovimentacoesEstoque.Add(movimentacaoEstoque);
            await _context.SaveChangesAsync();

            return movimentacaoEstoque;
        }

        private void ValidarDadosMovimentacaoEstoque(MovimentacaoEstoque movimentacaoEstoque)
        {
            if (movimentacaoEstoque.Quantidade <= 0 )
                throw new ValidationException("A quantidade de uma movimentação de estoque não pode ser menor ou igual a 0.");
            
            if (movimentacaoEstoque.TipoMovimentacao != TipoMovimentacao.Entrada && movimentacaoEstoque.TipoMovimentacao != TipoMovimentacao.Saida)
                throw new ValidationException("Tipo de movimentação inválido.");
        }

        private async Task<Produto> ObterProdutoPorIdAsync(long produtoId)
        {
            if (produtoId <= 0)
                throw new ValidationException("O Id do produto não pode ser menor ou igual a 0.");

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == produtoId);
            
            if (produto is null)
                throw new NotFoundException("Nenhum produto encontrado para o Id informado.");
            
            return produto;
        }

        private void ValidarEstoqueParaSaida(MovimentacaoEstoque movimentacaoEstoque, Produto produto)
        {
            if (movimentacaoEstoque.TipoMovimentacao == TipoMovimentacao.Saida 
                && produto.Quantidade < movimentacaoEstoque.Quantidade)
                throw new BusinessException("Estoque insuficiente para realizar esta operação.");
        }

        private void EfetuarOperacaoDeEstoque(MovimentacaoEstoque movimentacaoEstoque, int quantidade)
        {
            switch (movimentacaoEstoque.TipoMovimentacao)
            {
                case TipoMovimentacao.Entrada:
                    quantidade += movimentacaoEstoque.Quantidade;
                    break;

                case TipoMovimentacao.Saida:
                    quantidade -= movimentacaoEstoque.Quantidade;
                    break;
                
                default:
                    throw new ValidationException("Tipo de movimentação inválido.");
            }
        }

        public async Task<List<MovimentacaoEstoque>> GetAll()
        {
            var movimentacao = await _context.MovimentacoesEstoque.ToListAsync();

            return movimentacao;
        }

        public async Task<MovimentacaoEstoque> GetById(long id)
        {
            if (id <= 0)
                throw new ValidationException("O Id informado não pode ser menor o igual a 0.");
            
            var movimentacao = await _context.MovimentacoesEstoque
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (movimentacao is null)
                throw new NotFoundException("Nenhuma movimentação encontrada para o Id informada.");
            
            return movimentacao;
        }
    }
}