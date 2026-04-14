using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Model;

namespace EstoqueApi.DTOs
{
    public class CategoriaResponse
    {
        public long Id { get; }
        public string Nome { get; }
        public ICollection<ProdutoResponse> Produtos { get; } = new List<ProdutoResponse>();

        public CategoriaResponse(long id, string nome, ICollection<ProdutoResponse> produtos)
        {
            Id = id;
            Nome = nome;
            Produtos = produtos;
        }

    }
}