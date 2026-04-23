using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.Model;
using EstoqueApi.Service;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApiTestesUnitarios.Tests.ProdutoTestes
{
    public class Delete_Sucesso_Tests
    {
        private AppDbContext CriarBancoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task DeleteAsync_IdValido_DeveRemoverProdutoDoBanco()
        {
            var context = CriarBancoEmMemoria();
            var service = new ProdutoService(context);

            var categoria = new Categoria("Bebidas");
            context.Categorias.Add(categoria);

            var produto = new Produto("Coca-cola", 0, categoria.Id);
            context.Produtos.Add(produto);
            await context.SaveChangesAsync();

            await service.DeleteAsync(produto.Id);

            var validarAlteracoesNoBanco = await context.Produtos.FirstOrDefaultAsync(p => p.Id == produto.Id);
            Assert.Null(validarAlteracoesNoBanco);

            var existeRegistro = await context.Produtos.AnyAsync();
            Assert.False(existeRegistro);
        }
    }
}