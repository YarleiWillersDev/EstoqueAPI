using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Exceptions;
using EstoqueApi.Model;
using EstoqueApi.Service;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApiTestesUnitarios.Tests.ProdutoTestes
{
    public class Create_Validation_Tests
    {
        private AppDbContext CriarBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_RequestNull_DeveRetornarExcecao()
        {
            var context = CriarBancoEmMemoria();
            var service = new ProdutoService(context);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(null!));
            Assert.Contains("produto", exception.Message.ToLowerInvariant());

            var naoExisteRegistroNoBanco = await context.Produtos.AnyAsync();
            Assert.False(naoExisteRegistroNoBanco);
        }

        [Fact]
        public async Task CreateAsync_NomeNull_DeveRetornarExcecao()
        {
            var context = CriarBancoEmMemoria();
            var service = new ProdutoService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            var produtoRequest = new ProdutoRequest
            {
                Nome = null!,
                Quantidade = 0,
                CategoriaId = categoria.Id
            };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(produtoRequest));
            Assert.Contains("nome do produto", exception.Message.ToLowerInvariant());

            var naoExisteRegistroNoBanco = await context.Produtos.AnyAsync();
            Assert.False(naoExisteRegistroNoBanco);
        }

        [Fact]
        public async Task CreateAsync_NomeVazio_DeveRetornarExcecao()
        {
            var context = CriarBancoEmMemoria();
            var service = new ProdutoService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            var produtoRequest = new ProdutoRequest
            {
                Nome = "       ",
                Quantidade = 0,
                CategoriaId = categoria.Id
            };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(produtoRequest));
            Assert.Contains("nome do produto", exception.Message.ToLowerInvariant());

            var naoExisteRegistroNoBanco = await context.Produtos.AnyAsync();
            Assert.False(naoExisteRegistroNoBanco);
        }

        [Fact]
        public async Task CreateAsync_QuantidadeNegativa_DeveRetornarExcecao()
        {
            var context = CriarBancoEmMemoria();
            var service = new ProdutoService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            var produtoRequest = new ProdutoRequest
            {
                Nome = "Coca-cola",
                Quantidade = -1,
                CategoriaId = categoria.Id
            };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(produtoRequest));
            Assert.Contains("quantidade do produto", exception.Message.ToLowerInvariant());

            var naoExisteRegistroNoBanco = await context.Produtos.AnyAsync();
            Assert.False(naoExisteRegistroNoBanco);
        }

        [Fact]
        public async Task CreateAsync_IdCategoriaInvalido_DeveRetornarExcecao()
        {
            var context = CriarBancoEmMemoria();
            var service = new ProdutoService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);

            var produtoRequest = new ProdutoRequest
            {
                Nome = "Coca-cola",
                Quantidade = 0,
                CategoriaId = 0  
            };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(produtoRequest));
            Assert.Contains("id da categoria", exception.Message.ToLowerInvariant());

            var naoExisteRegistroNoBanco = await context.Produtos.AnyAsync();
            Assert.False(naoExisteRegistroNoBanco);
        }

        [Fact]
        public async Task CreateAsync_CategoriaInexistente_DeveRetornarExcecao()
        {
            var context = CriarBancoEmMemoria();
            var service = new ProdutoService(context);

            var produtoRequest = new ProdutoRequest
            {
                Nome = "Coca-cola",
                Quantidade = 0,
                CategoriaId = 1
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => service.CreateAsync(produtoRequest));
            Assert.Contains("não existem categorias", exception.Message.ToLowerInvariant());

            var naoExisteRegistroNoBanco = await context.Produtos.AnyAsync();
            Assert.False(naoExisteRegistroNoBanco);
        }
    }
}