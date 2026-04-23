using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.Exceptions;
using EstoqueApi.Service;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApiTestesUnitarios.Tests.ProdutoTestes
{
    public class Delete_Validacao_Tests
    {
        private AppDbContext CriarBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task DeleteAsync_IdNegativo_DeveRetornarExcecao()
        {
            var context = CriarBancoEmMemoria();
            var service = new ProdutoService(context);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.DeleteAsync(-1));
            Assert.Contains("id", exception.Message.ToLowerInvariant());

            var existeRegistro = await context.Produtos.AnyAsync();
            Assert.False(existeRegistro);
        }

        [Fact]
        public async Task DeleteAsync_IdInexistente_DeveRetornarExcecao()
        {
            var context = CriarBancoEmMemoria();
            var service = new ProdutoService(context);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(999L));
            Assert.Contains("nenhum produto", exception.Message.ToLowerInvariant());

            var existeRegistro = await context.Produtos.AnyAsync();
            Assert.False(existeRegistro);
        }
    }
}