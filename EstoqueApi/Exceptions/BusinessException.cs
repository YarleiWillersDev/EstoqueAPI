using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Exceptions
{
    public class BusinessException : AppException
    {
        public BusinessException(string message)
        : base(422, "Regra de negócio violada", message) { }

    }
}