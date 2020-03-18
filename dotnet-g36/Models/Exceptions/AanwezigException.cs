using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Exceptions
{
    public class AanwezigException : Exception
    {
        public AanwezigException() { }
        public AanwezigException(string message) : base(message) { }
    }
}
