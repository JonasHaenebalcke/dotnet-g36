using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Exceptions
{
    public class AlIngeschrevenException : Exception
    {
        public AlIngeschrevenException() { }
        public AlIngeschrevenException(string message) : base(message) { }
    }
}
