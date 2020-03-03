using System;
using System.Collections.Generic;

namespace dotnet_g36.Models.Exceptions
{
    public class GeenActieveGebruikerException : Exception
    {
        public GeenActieveGebruikerException() { }
        public GeenActieveGebruikerException(string message) : base(message) { }

    }
}
