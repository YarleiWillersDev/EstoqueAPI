using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Exceptions;
using EstoqueApi.Service;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;

namespace EstoqueApiTestesUnitarios.Tests.CategoriaTestes
{
    public class CriarCategoriaTeste
    {

        private AppDbContext CriarBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_RequestCorreto_DeveRetornarSucesso()
        {
            
            //Arrange
            var context = CriarBancoEmMemoria();
            var service = new CategoriaService(context);

            var request = new CategoriaRequest
            {
                Nome = "Bebidas"
            };

            // Act
            var result = await service.CreateAsync(request);

            // Assert (resultado)
            Assert.NotNull(result); // Valida se o resultado não é null
            Assert.Equal("Bebidas", result.Nome); // Valida se o campo Nome é igual ao request
            Assert.True(result.Id > 0); // Validad se o ID é maior que 0
            
            // Assert (Dados no Banco)
            var categoriaSalvaNoBanco = await context.Categorias.FirstOrDefaultAsync();

            Assert.NotNull(categoriaSalvaNoBanco);
            Assert.Equal("Bebidas", categoriaSalvaNoBanco.Nome);
        }

        [Fact]
        public async Task CreateAsync_RequestNull_DeveLancarValidationException()
        {
            var context = CriarBancoEmMemoria();
            var service = new CategoriaService(context);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(null!));
            Assert.Equal("A categoria não pode ser nula", exception.Message); // Valida se exceção de Request null está funcionando

            var validarSeExisteRegistroNoBanco = await context.Categorias.AnyAsync();
            Assert.False(validarSeExisteRegistroNoBanco);
        }

        [Fact]
        public async Task CreateAsync_NomeNull_DeveLancarValidationException()
        {
            var context = CriarBancoEmMemoria(); // Cria banco em memória
            var service = new CategoriaService(context); // Cria instancia do service

            var request = new CategoriaRequest // Request do método
            {
                Nome = null!
            };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(request)); // Valida se método vai lançar exceção
            Assert.Equal("O nome da categoria não pode ser null.", exception.Message); // Valida se a mensagem retornada condiz com o esperado

            var existeRegistro = await context.Categorias.AnyAsync(); // Valida se o testes gravou registro no banco
            Assert.False(existeRegistro); 
        }

        [Fact]
        public async Task CreateAsync_NomeVazio_DeveLancarValidationException()
        {
            var context = CriarBancoEmMemoria();
            var service = new CategoriaService(context);

            var request = new CategoriaRequest
            {
                Nome = ""
            };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(request));
            Assert.Equal("O nome da categoria não pode ser null.", exception.Message);

            var existeRegistro = await context.Categorias.AnyAsync();
            Assert.False(existeRegistro);
        }

        [Fact]
        public async Task CreateAsync_NomeDeEspacos_DeveLancarValidationException()
        {
            var context = CriarBancoEmMemoria();
            var service = new CategoriaService(context);

            var request = new CategoriaRequest
            {
                Nome = "           "
            };

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(request));
            Assert.Equal("O nome da categoria não pode ser null.", exception.Message);

            var existeRegistro = await context.Categorias.AnyAsync();
            Assert.False(existeRegistro);
        }
    }
}