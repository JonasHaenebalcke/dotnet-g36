using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36.Models.Domain
{
    public class Feedback
    {
        #region properties
        public int FeedbackID { get; set; }
        public string NaamAuteur { get; set; }
        public string Tekst { get; set; }
        public DateTime TimeWritten { get; set; }

        public User User
        {
            get => default;
            set
            {
            }
        }
        #endregion
    }
}