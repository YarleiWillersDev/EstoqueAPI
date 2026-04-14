using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Exceptions
{
    public abstract class AppException : Exception
    {

        public int StatusCode { get; protected init; }
        public string Title { get; protected init; } = string.Empty;

        public AppException(int statusCode, string title, string message) : base(message)
        {
            StatusCode = statusCode;
            Title = title;
        }

        public AppException() { }
    }
}