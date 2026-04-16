using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.DTOs
{
    public class ProdutoSimplesResponse
    {
        public long Id { get; }
        public string Nome { get; } = null!;

        public ProdutoSimplesResponse(long id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}