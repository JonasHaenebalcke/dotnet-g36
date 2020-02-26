using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public string Tekst { get; set; }
        public DateTime TimeWritten { get; set; }

        public User User
        {
            get => default;
            set
            {
            }
        }
    }
}