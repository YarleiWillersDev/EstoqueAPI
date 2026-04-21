using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Model;
using EstoqueApi.Service;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApiTestesUnitarios.Tests.MovimentacaoEstoqueTestes.Create
{
    public class Create_Sucesso_Tests
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
        public async Task CreateAsync_SaidaValida_DeveDecrementarEstoqueEPersistirMovimentacao()
        {
            var context = CriarBancoEmMemoria();
            var service = new MovimentacaoEstoqueService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);

            var produto = new Produto("Coca-cola", 50, categoria.Id);
            context.Produtos.Add(produto);
            await context.SaveChangesAsync();

            var request = new MovimentacaoEstoqueRequest
            {
                Quantidade = 30,
                TipoMovimentacao = TipoMovimentacao.Saida,
                ProdutoId = produto.Id
            };

            var dataAntesDaOperação = DateTime.UtcNow;

            var response = await service.CreateAsync(request);

            var dataPosretiorOperacao = DateTime.UtcNow;

            Assert.NotNull(response);
            Assert.Equal(30, response.Quantidade);
            Assert.Equal(TipoMovimentacao.Saida, response.TipoMovimentacao);
            Assert.Equal(produto.Id, response.ProdutoId);
            Assert.True(response.Id > 0);
            Assert.InRange(response.DataMovimentacao, dataAntesDaOperação, dataPosretiorOperacao);

            var movimentacaoExiste = await context.MovimentacoesEstoque.FirstOrDefaultAsync();
            Assert.NotNull(movimentacaoExiste);
            Assert.Equal(30, movimentacaoExiste.Quantidade);
            Assert.Equal(TipoMovimentacao.Saida, movimentacaoExiste.TipoMovimentacao);
            Assert.Equal(produto.Id, movimentacaoExiste.ProdutoId);

            var produtoAtualizado = await context.Produtos.FirstAsync();
            Assert.Equal(20, produtoAtualizado.Quantidade);

        }
    }
}