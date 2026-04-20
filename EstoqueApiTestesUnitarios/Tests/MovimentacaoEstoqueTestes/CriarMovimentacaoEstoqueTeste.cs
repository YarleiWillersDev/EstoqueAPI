using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Exceptions;
using EstoqueApi.Model;
using EstoqueApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApiTestesUnitarios.Tests.MovimentacaoEstoqueTestes
{
    public class CriarMovimentacaoEstoqueTeste
    {
        private AppDbContext CriarBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_EntradaValida_DeveIncrementarEstoqueEPersistirMovimentacao()
        {
            var context = CriarBancoEmMemoria();
            var service = new MovimentacaoEstoqueService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);

            var produto = new Produto("Coca-Cola", 0, categoria.Id);
            context.Produtos.Add(produto);
            await context.SaveChangesAsync();

            var request = new MovimentacaoEstoqueRequest
            {
                Quantidade = 50,
                TipoMovimentacao = TipoMovimentacao.Entrada,
                ProdutoId = produto.Id
            };

            var dataMovimentacaoAntesDeCriar = DateTime.UtcNow; // Valida momento de execução antes de criar movimentação

            var response = await service.CreateAsync(request);

            var dataMovimentacaoDepoisDeCriar = DateTime.UtcNow; // Valida momento de execução depois de criar movimentação

            Assert.NotNull(response);
            Assert.Equal(50, response.Quantidade);
            Assert.Equal(TipoMovimentacao.Entrada, response.TipoMovimentacao);
            Assert.Equal(produto.Id, response.ProdutoId);
            Assert.True(response.Id > 0);
            Assert.InRange(response.DataMovimentacao, dataMovimentacaoAntesDeCriar, dataMovimentacaoDepoisDeCriar);

            var movimentacaoExiste = await context.MovimentacoesEstoque.FirstOrDefaultAsync();
            Assert.NotNull(movimentacaoExiste);
            Assert.Equal(50, movimentacaoExiste.Quantidade);
            Assert.Equal(TipoMovimentacao.Entrada, movimentacaoExiste.TipoMovimentacao);
            Assert.Equal(produto.Id, movimentacaoExiste.ProdutoId);

            var produtoAtualizado = await context.Produtos.FirstAsync();
            Assert.Equal(50, produtoAtualizado.Quantidade);

        }

        [Fact]
        public async Task CreateAsync_EntradaInvalida_DeveRetornarErroDevidoRequestNull()
        {
            
            var context = CriarBancoEmMemoria();
            var service = new MovimentacaoEstoqueService(context);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(null!));
            Assert.Equal("Movimentação de estoque não pode ser nula.", exception.Message);

            var naoExisteNoBanco = await context.MovimentacoesEstoque.AnyAsync();
            Assert.False(naoExisteNoBanco);

        }

        [Fact]
        public async Task CreateAsync_EntradaInvalida_DeveRetornarErroDevidoQuantidadeInvalida()
        {
            var context = CriarBancoEmMemoria();
            var service = new MovimentacaoEstoqueService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);

            var produto = new Produto("Coca-cola", 0, categoria.Id);
            context.Produtos.Add(produto);
            await context.SaveChangesAsync();

            var request = new MovimentacaoEstoqueRequest
            {
                Quantidade = -1,
                TipoMovimentacao = TipoMovimentacao.Entrada,
                ProdutoId = produto.Id
            };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(request));
            Assert.Equal("A quantidade de uma movimentação de estoque não pode ser menor ou igual a 0.", exception.Message);

            var naoExisteNoBanco = await context.MovimentacoesEstoque.AnyAsync();
            Assert.False(naoExisteNoBanco);

            Assert.Equal(0 , produto.Quantidade);
        }
    }
}