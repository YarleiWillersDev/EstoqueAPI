using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Exceptions;
using EstoqueApi.Service;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApiTestesUnitarios.Tests.CategoriaTestes
{
    public class Create_Validacao
    {
        private AppDbContext CriarBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_RequestNull_DeveLancarValidationException()
        {
            var context = CriarBancoEmMemoria();
            var service = new CategoriaService(context);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(null!));
            Assert.Contains("categoria", exception.Message.ToLowerInvariant()); // Valida se exceção de Request null está funcionando

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
            Assert.Contains("categoria", exception.Message.ToLowerInvariant()); // Valida se a mensagem retornada condiz com o esperado

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
            Assert.Contains("categoria", exception.Message.ToLowerInvariant());

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
            Assert.Contains("categoria", exception.Message.ToLowerInvariant());

            var existeRegistro = await context.Categorias.AnyAsync();
            Assert.False(existeRegistro);
        }
    }
}