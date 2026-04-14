using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Exceptions
{
    public class ValidationException : AppException
    {
        public ValidationException(string message)
        : base(400, "Parametro inválido", message) { }

    }
}