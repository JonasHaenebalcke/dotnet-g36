using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Exceptions
{
    public class FeedbackException : Exception
    {
        public FeedbackException() { }
        public FeedbackException(string message) : base(message) { }
    }
}

