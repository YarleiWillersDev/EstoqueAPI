using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EstoqueApi.DTOs
{
    public class ProdutoResponse
    {
        public long Id { get; }
        public string Nome { get; }
        public int Quantidade { get; }
        public long CategoriaId { get; }
        public ICollection<MovimentacaoEstoqueResponse> Movimentacoes { get; } 
        
        public ProdutoResponse(long id, string nome, int quantidade, long categoriaId, ICollection<MovimentacaoEstoqueResponse> movimentacoes)
        {
            Id = id;
            Nome = nome;
            Quantidade = quantidade;
            CategoriaId = categoriaId;
            Movimentacoes = movimentacoes;
        }
    }
}