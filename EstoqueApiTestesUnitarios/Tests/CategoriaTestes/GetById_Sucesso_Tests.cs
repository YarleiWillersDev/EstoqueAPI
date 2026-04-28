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
    public class GetById_Sucesso_Tests
    {
        /*private AppDbContext CriaBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetById_EntradaValida_DeveRetornarCategoria()
        {
            var context = CriaBancoEmMemoria();
            var service = new CategoriaService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();
            
            var response = await service.GetByIdAsync(categoria.Id);

            Assert.NotNull(response);
            Assert.Equal(categoria.Nome, response.Nome);
            Assert.Equal(categoria.Id, response.Id);
            Assert.True(response.Id > 0);
        
        }*/
    }
}