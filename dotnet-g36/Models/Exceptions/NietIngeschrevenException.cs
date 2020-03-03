using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Exceptions
{
    public class NietIngeschrevenException : Exception
    {
        public NietIngeschrevenException() { }
        public NietIngeschrevenException(string message) : base(message) { }
    }
}
