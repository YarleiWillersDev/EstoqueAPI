using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Model;
using EstoqueApi.Repositories;
using EstoqueApi.Service;
using Microsoft.EntityFrameworkCore;
using Moq;
namespace EstoqueApiTestesUnitarios.Tests.ProdutoTestes
{
    public class Create_Sucesso_Tests
    {
        [Fact]
        public async Task CreateAsync_EntradaValida_DeveRetornarSucesso()
        {
            var categoria = new Categoria("Bebidas");

            var mockCategoriaRepository = new Mock<ICategoriaRepository>();
            var mockProdutoRepository = new Mock<IProdutoRepository>();

            var produtoRequest = new ProdutoRequest
            {
                Nome = "Coca-cola",
                Quantidade = 0,
                CategoriaId = 1
            };

            mockCategoriaRepository
                .Setup(c => c.GetByIdAsync(produtoRequest.CategoriaId))
                .ReturnsAsync(categoria);

            mockProdutoRepository
                .Setup(p => p.AddAsync(It.IsAny<Produto>()))
                .Returns(Task.CompletedTask);

            var service = new ProdutoService(mockProdutoRepository.Object, mockCategoriaRepository.Object);

            var response = await service.CreateAsync(produtoRequest);

            Assert.NotNull(response);
            Assert.Equal(produtoRequest.Nome, response.Nome);
            Assert.Equal(produtoRequest.Quantidade, response.Quantidade);
            Assert.Equal(produtoRequest.CategoriaId, response.CategoriaId);

            mockCategoriaRepository.Verify(c => c.GetByIdAsync(produtoRequest.CategoriaId), Times.Once);
            mockProdutoRepository.Verify(p => p.AddAsync(It.IsAny<Produto>()), Times.Once);
        }
    }
}