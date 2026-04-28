using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.Model;
using EstoqueApi.Service;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApiTestesUnitarios.Tests.CategoriaTestes
{
    public class Delete_Sucesso_Tests
    {
        /*private AppDbContext CriarBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task DeleteAsync_DeveDeletarCategoriaAtravesDoId()
        {
            var context = CriarBancoEmMemoria();
            var service = new CategoriaService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            await service.DeleteAsync(categoria.Id);

            var buscarEntidadeNoBanco = await context.Categorias.FirstOrDefaultAsync(c => c.Id == categoria.Id);
            Assert.Null(buscarEntidadeNoBanco);
        }*/
    }
}