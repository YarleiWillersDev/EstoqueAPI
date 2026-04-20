using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Exceptions;
using EstoqueApi.Mappers;
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

        public async Task<MovimentacaoEstoqueResponse> CreateAsync(MovimentacaoEstoqueRequest request)
        {
            if (request is null)
                throw new ValidationException("Movimentação de estoque não pode ser nula.");
            
            ValidarDadosMovimentacaoEstoque(request);

            var movimentacaoEstoque = MovimentacaoEstoqueMapper.ToEntity(request);

            var produto = await ObterProdutoPorIdAsync(movimentacaoEstoque.ProdutoId);

            EfetuarOperacaoDeEstoque(movimentacaoEstoque, produto);

            _context.MovimentacoesEstoque.Add(movimentacaoEstoque);
            await _context.SaveChangesAsync();

            return MovimentacaoEstoqueMapper.ToResponse(movimentacaoEstoque);
        }

        private void ValidarDadosMovimentacaoEstoque(MovimentacaoEstoqueRequest request)
        {
            if (request.Quantidade <= 0 )
                throw new ValidationException("A quantidade de uma movimentação de estoque não pode ser menor ou igual a 0.");
            
            if (request.TipoMovimentacao != TipoMovimentacao.Entrada && request.TipoMovimentacao != TipoMovimentacao.Saida)
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

        private void EfetuarOperacaoDeEstoque(MovimentacaoEstoque movimentacaoEstoque, Produto produto)
        {
            switch (movimentacaoEstoque.TipoMovimentacao)
            {
                case TipoMovimentacao.Entrada:
                    produto.AdicionaEstoque(movimentacaoEstoque.Quantidade);
                    break;

                case TipoMovimentacao.Saida:
                    produto.RemoverEstoque(movimentacaoEstoque.Quantidade);
                    break;
                
                default:
                    throw new ValidationException("Tipo de movimentação inválido.");
            }
        }

        public async Task<List<MovimentacaoEstoqueResponse>> GetAllAsync()
        {
            var movimentacao = await _context.MovimentacoesEstoque.ToListAsync();

            return movimentacao.Select(MovimentacaoEstoqueMapper.ToResponse).ToList();
        }

        public async Task<MovimentacaoEstoqueResponse> GetByIdAsync(long id)
        {
            if (id <= 0)
                throw new ValidationException("O Id informado não pode ser menor o igual a 0.");
            
            var movimentacao = await _context.MovimentacoesEstoque
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (movimentacao is null)
                throw new NotFoundException("Nenhuma movimentação encontrada para o Id informada.");
            
            return MovimentacaoEstoqueMapper.ToResponse(movimentacao);
        }
    }
}