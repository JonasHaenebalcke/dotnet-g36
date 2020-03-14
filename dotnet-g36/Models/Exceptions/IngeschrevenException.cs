using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Exceptions
{
    public class IngeschrevenException : Exception
    {
        public IngeschrevenException() { }
        public IngeschrevenException(string message) : base(message) { }
    }
}
