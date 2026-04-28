using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.Exceptions;
using EstoqueApi.Model;
using EstoqueApi.Service;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApiTestesUnitarios.Tests.CategoriaTestes
{
    public class Delete_Validacao_Tests
    {
        /*private AppDbContext CriaBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            return new AppDbContext(options);
        }

        [Fact]
        public async Task DeleteAsync_IdInvalido_DeveLancarExcecao()
        {
            var context = CriaBancoEmMemoria();
            var service = new CategoriaService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            var exception = await Assert.ThrowsAsync<ValidationException>(() => service.DeleteAsync(-1L));
            Assert.Contains("id", exception.Message.ToLowerInvariant());

            var categoriaContinuaExistindoNoBanco = await context.Categorias.FirstOrDefaultAsync(c => c.Id == categoria.Id);
            Assert.NotNull(categoriaContinuaExistindoNoBanco);
            Assert.Equal(categoria.Id, categoriaContinuaExistindoNoBanco.Id);
            Assert.Equal(categoria.Nome, categoriaContinuaExistindoNoBanco.Nome);
        }

        [Fact]
        public async Task DeleteAsync_IdInexistente_DeveLancarExcecao()
        {
            var context = CriaBancoEmMemoria();
            var service = new CategoriaService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            var exception =  await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(999L));
            Assert.Contains("categoria", exception.Message.ToLowerInvariant());

            var categoriaContinuaExistindoNoBanco = await context.Categorias.FirstOrDefaultAsync(c => c.Id == categoria.Id);
            Assert.NotNull(categoriaContinuaExistindoNoBanco);
            Assert.Equal(categoria.Id, categoriaContinuaExistindoNoBanco.Id);
            Assert.Equal(categoria.Nome, categoriaContinuaExistindoNoBanco.Nome);
        }*/
    }
}