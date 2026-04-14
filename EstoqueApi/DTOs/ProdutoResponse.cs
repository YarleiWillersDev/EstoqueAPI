using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.DTOs
{
    public class ProdutoResponse
    {
        public long Id { get; }
        public string Nome { get; }
        public int Quantidade { get; }
        public long CategoriaId { get; }
        
        public ProdutoResponse(long id, string nome, int quantidade, long categoriaId)
        {
            Id = id;
            Nome = nome;
            Quantidade = quantidade;
            CategoriaId = categoriaId;
        }
    }
}