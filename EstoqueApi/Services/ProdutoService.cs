using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.DTOs;
using EstoqueApi.Exceptions;
using EstoqueApi.Mappers;
using EstoqueApi.Model;
using EstoqueApi.Repositories;

namespace EstoqueApi.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<ProdutoResponse> CreateAsync(ProdutoRequest request)
        {
            if (request is null)
                throw new ValidationException("O produto não pode ser null"); 

            ValidarProdutoCreate(request);
            await ValidarCategoriaExisteAsync(request.CategoriaId);

            var produto = ProdutoMapper.ToEntity(request);

            await _produtoRepository.AddAsync(produto);

            return ProdutoMapper.ToResponse(produto);
        }

        private void ValidarProdutoCreate(ProdutoRequest produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
                throw new ValidationException("O nome do produto não pode ser vazio.");

            if (produto.Quantidade < 0)
                throw new ValidationException("A quantidade do produto não pode ser menor que 0.");

            if (produto.CategoriaId <= 0)
                throw new ValidationException("O ID da categoria não pode ser menor ou igual a 0");
        }

        private async Task ValidarCategoriaExisteAsync(long categoriaId)
        {
            var categoriaExiste = await _categoriaRepository.GetByIdAsync(categoriaId);

            if (categoriaExiste is null)
                throw new NotFoundException("Não existem categorias cadastradas para o ID informado.");
        }

        public async Task DeleteAsync(long id)
        {
            if (id <= 0)
                throw new ValidationException("Id inválido");

            var produto = await ValidarProdutoExistenteAsync(id);

            await _produtoRepository.DeleteAsync(produto);
        }

        public async Task<List<ProdutoSimplesResponse>> GetAllAsync()
        {
            var produtos = await _produtoRepository.GetAllAsync();

            return produtos.Select(ProdutoMapper.ToSimplesResponse).ToList();
        }

        public async Task<List<ProdutoSimplesResponse>> GetByCategoriaIdAsync(long categoriaId)
        {
            if (categoriaId <= 0)
                throw new ValidationException("O Id da categoira não pode ser maior ou igual a 0.");
            
            await ValidarCategoriaExisteAsync(categoriaId);

            var produtos = await _produtoRepository.GetByCategoriaIdAsync(categoriaId);

            return produtos.Select(ProdutoMapper.ToSimplesResponse).ToList();
        }

        public async Task<ProdutoResponse> GetByIdAsync(long id)
        {
            if (id <= 0)
                throw new ValidationException("O ID não pode ser menor ou igual a 0.");

            var produto = await _produtoRepository.GetByIdAsync(id);

            if (produto is null)
                throw new NotFoundException("Nenhum produto encontrado para o ID informado.");

            return ProdutoMapper.ToResponse(produto);
        }

        public async Task<ProdutoResponse> UpdateAsync(ProdutoAtualizarRequest produtoRequest, long idProdutoExistente)
        {
            if (produtoRequest is null)
                throw new ValidationException("Dados inválidos para atualização do produto");

            ValidarProdutoUpdate(produtoRequest);

            var produtoExistente = await ValidarProdutoExistenteAsync(idProdutoExistente);

            if (produtoExistente.CategoriaId != produtoRequest.CategoriaId)
            {
                await ValidarCategoriaExisteAsync(produtoRequest.CategoriaId);
            }

            produtoExistente.Atualizar(produtoRequest.Nome, produtoRequest.CategoriaId);

            await _produtoRepository.UpdateAsync(produtoExistente);

            return ProdutoMapper.ToResponse(produtoExistente);
        }

        private void ValidarProdutoUpdate(ProdutoAtualizarRequest produtoRequest)
        {
            if (string.IsNullOrWhiteSpace(produtoRequest.Nome))
                throw new ValidationException("O nome do produto é obrigatório");
            
            if (produtoRequest.CategoriaId <= 0)
                throw new ValidationException("O ID da categoria não pode ser menor ou igual a 0");
        }

        private async Task<Produto> ValidarProdutoExistenteAsync(long id)
        {
            var produtoExistente = await _produtoRepository.GetByIdAsync(id);

            if (produtoExistente is null)
                throw new NotFoundException("Nenhum produto foi encontrado para o Id informado.");

            return produtoExistente;
        }
    }
}