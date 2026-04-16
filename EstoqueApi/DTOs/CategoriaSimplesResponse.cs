using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.DTOs
{
    public class CategoriaSimplesResponse
    {
        
        public long Id { get; }
        public string Nome { get; } = null!;

        public CategoriaSimplesResponse(long id, string nome)
        {
            Id = id;
            Nome = nome;
        }

    }
}