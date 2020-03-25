using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Exceptions
{
    public class SchrijfInSchrijfUitException : Exception
    {
        public SchrijfInSchrijfUitException() { }
        public SchrijfInSchrijfUitException(string message) : base(message) { }
    }
}
