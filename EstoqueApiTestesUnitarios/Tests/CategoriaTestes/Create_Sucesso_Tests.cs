using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Service;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApiTestesUnitarios.Tests.CategoriaTestes
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
    }
}