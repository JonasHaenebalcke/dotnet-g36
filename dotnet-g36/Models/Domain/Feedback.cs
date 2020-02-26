using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36
{
    public class Feedback
    {
        public string tekst { get; set; }
        public DateTime timeWritten { get; set; }

        public User User
        {
            get => default;
            set
            {
            }
        }
    }
}