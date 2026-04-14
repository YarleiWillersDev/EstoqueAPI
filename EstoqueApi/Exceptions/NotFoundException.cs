using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message = "Nenhum registro foi encontrado para os valores informados.")
        : base(404, "Dados não encontrados", message) { }

    }
}