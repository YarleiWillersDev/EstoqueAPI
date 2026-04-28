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
    public class GetAll_Sucesso_Tests
    {
        /*private AppDbContext CriarBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAll_DeveRetornarListaDeCategorias()
        {
            var context = CriarBancoEmMemoria();
            var service = new CategoriaService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            var response = await service.GetAllAsync();

            Assert.NotEmpty(response);

            // Valida se o elemento da lista está correto
            Assert.Collection(response, c1 =>
            {
                Assert.Equal(categoria.Id, c1.Id);
                Assert.Equal(categoria.Nome, c1.Nome);
            });
        }

        [Fact]
        public async Task GetAll_DeveRetornarListaDeCategoriasComMaisRegistros()
        {
            var context = CriarBancoEmMemoria();
            var service = new CategoriaService(context);

            var categoria1 = new Categoria("Bebidas");
            var categoria2 = new Categoria("Alimentos");
            var categoria3 = new Categoria("Medicamentos");

            context.Categorias.AddRange(categoria1, categoria2, categoria3);
            await context.SaveChangesAsync();

            var response = await service.GetAllAsync();

            Assert.NotEmpty(response);
            Assert.Equal(3, response.Count());

            Assert.Collection(response,
                c1 => Assert.Equal(categoria1.Nome, c1.Nome),
                c2 => Assert.Equal(categoria2.Nome, c2.Nome),
                c3 => Assert.Equal(categoria3.Nome, c3.Nome));

        }

        [Fact]
        public async Task GetAll_DeveRetornarListaVazia()
        {
            var context = CriarBancoEmMemoria();
            var service = new CategoriaService(context);

            var response = await service.GetAllAsync();

            Assert.NotNull(response);
            Assert.Empty(response);
        }*/
    }
}