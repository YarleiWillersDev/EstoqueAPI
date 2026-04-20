using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Exceptions
{
    public class BussinesException : AppException
    {
        public BussinesException(string message)
        : base(422, "Regra de negócio violada", message) { }

    }
}