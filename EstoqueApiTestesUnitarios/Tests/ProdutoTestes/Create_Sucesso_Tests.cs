using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Model;
using EstoqueApi.Service;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApiTestesUnitarios.Tests.ProdutoTestes
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
        public async Task CreateAsync_EntradaValida_DeveRetornarSucesso()
        {
            var context = CriarBancoEmMemoria();
            var service = new ProdutoService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            var produtoRequest = new ProdutoRequest
            {
                Nome = "Coca-cola",
                Quantidade = 0,
                CategoriaId = categoria.Id
            };

            var response = await service.CreateAsync(produtoRequest);

            Assert.NotNull(response);
            Assert.Equal(response.Nome, produtoRequest.Nome);
            Assert.Equal(response.Quantidade, produtoRequest.Quantidade);
            Assert.Equal(response.CategoriaId, categoria.Id);
            Assert.True(response.Id > 0);

            var existeNoBanco = await context.Produtos.FirstOrDefaultAsync(p => p.Id == response.Id);
            Assert.NotNull(existeNoBanco);
            Assert.Equal(existeNoBanco.Nome, response.Nome);
            Assert.Equal(existeNoBanco.Quantidade, response.Quantidade);
            Assert.Equal(existeNoBanco.CategoriaId, categoria.Id);
        }
    }
}