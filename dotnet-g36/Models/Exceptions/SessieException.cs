using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Exceptions
{
    public class SessieException : Exception
    {
        public SessieException() { }
        public SessieException(string message):base(message) { }

    }
}
