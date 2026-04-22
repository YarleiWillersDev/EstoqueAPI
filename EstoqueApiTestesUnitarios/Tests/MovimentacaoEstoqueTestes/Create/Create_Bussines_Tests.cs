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
using SQLitePCL;

namespace EstoqueApiTestesUnitarios.Tests.MovimentacaoEstoqueTestes
{
    public class Create_Bussines_Tests
    {
        private AppDbContext CriarBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_EntradaInvalida_DeveRetornarErroAoDescontarEstoqueDeProdutoSemEstoqueSuficiente()
        {
            var context = CriarBancoEmMemoria();
            var service = new MovimentacaoEstoqueService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);

            var produto = new Produto("Coca-cola", 30, categoria.Id);
            context.Produtos.Add(produto);
            await context.SaveChangesAsync();

            var request = new MovimentacaoEstoqueRequest
            {
                Quantidade = 40,
                TipoMovimentacao = TipoMovimentacao.Saida,
                ProdutoId = produto.Id
            };

            var exception = await Assert.ThrowsAsync<BussinesException>(() => service.CreateAsync(request));
            Assert.Contains("estoque", exception.Message.ToLowerInvariant());

            var naoExisteNoBanco = await context.MovimentacoesEstoque.AnyAsync();
            Assert.False(naoExisteNoBanco);
            Assert.Equal(30, produto.Quantidade);
        }
    }
}