using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Exceptions;
using EstoqueApi.Model;
using EstoqueApi.Service;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApiTestesUnitarios.Tests.MovimentacaoEstoqueTestes.Create
{
    public class Create_Validacao_Tests
    {
        private AppDbContext CriarBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_EntradaInvalida_DeveRetornarErroDevidoRequestNull()
        {

            var context = CriarBancoEmMemoria();
            var service = new MovimentacaoEstoqueService(context);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(null!));
            Assert.Contains("movimentação", exception.Message.ToLowerInvariant());

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
            Assert.Contains("movimentação", exception.Message.ToLowerInvariant());

            var naoExisteNoBanco = await context.MovimentacoesEstoque.AnyAsync();
            Assert.False(naoExisteNoBanco);

            Assert.Equal(0, produto.Quantidade);
        }

        [Fact]
        public async Task CreateAsync_EntradaInvalida_DeveRetornarErroAoCriarMovimentacaoComTipoMovimentacaoInvalido()
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
                Quantidade = 50,
                TipoMovimentacao = (TipoMovimentacao)999,
                ProdutoId = produto.Id
            };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(request));
            Assert.Contains("movimentação", exception.Message.ToLowerInvariant());

            var naoExisteNoBanco = await context.MovimentacoesEstoque.AnyAsync();
            Assert.False(naoExisteNoBanco);
        }

        [Fact]
        public async Task CreateAsync_EntradaInvalida_DeveRetornarErroAoCriarMovimentacaoComProdutoIdInvalido()
        {
            var context = CriarBancoEmMemoria();
            var service = new MovimentacaoEstoqueService(context);

            var request = new MovimentacaoEstoqueRequest
            {
                Quantidade = 50,
                TipoMovimentacao = TipoMovimentacao.Entrada,
                ProdutoId = -1
            };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(request));
            Assert.Contains("id", exception.Message.ToLowerInvariant());

            var naoExisteNoBanco = await context.MovimentacoesEstoque.AnyAsync();
            Assert.False(naoExisteNoBanco);
        }

        [Fact]
        public async Task CreateAsync_EntradaInvalida_DeveRetornarErroAoCriarMovimentacaoComProdutoNaoEncontrado()
        {
            var context = CriarBancoEmMemoria();
            var service = new MovimentacaoEstoqueService(context);

            var request = new MovimentacaoEstoqueRequest
            {
                Quantidade = 50,
                TipoMovimentacao = TipoMovimentacao.Entrada,
                ProdutoId = 999L
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => service.CreateAsync(request));
            Assert.Contains("id", exception.Message.ToLowerInvariant());

            var naoExisteNoBanco = await context.MovimentacoesEstoque.AnyAsync();
            Assert.False(naoExisteNoBanco);
        }
    }
}