using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Exceptions;
using EstoqueApi.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace EstoqueApiTestesUnitarios.Tests.CategoriaTestes
{
    public class GetById_Validacao_Tests
    {
        private AppDbContext CriarBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetById_IdInvalido_DeveLancarValidationException()
        {
            var context = CriarBancoEmMemoria();
            var service = new CategoriaService(context);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.GetByIdAsync(-1));
            Assert.Contains("Id", exception.Message);
        }

        [Fact]
        public async Task GetById_IdNaoEncontrado_DeveLancarNotFoundException()
        {
            var context = CriarBancoEmMemoria();
            var service = new CategoriaService(context);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => service.GetByIdAsync(999L));
            Assert.Contains("Categoria", exception.Message);
        }
    }
}