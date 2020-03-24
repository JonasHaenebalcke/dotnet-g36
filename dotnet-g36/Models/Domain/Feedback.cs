using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36.Models.Domain
{
    public class Feedback
    {
        #region properties
        public int FeedbackID { get; set; }
        public string Tekst { get; set; }
        public DateTime TimeWritten { get; set; }
        public Gebruiker Auteur { get; set; }
        public int Score { get; set; }
        #endregion

        #region constructor

        public Feedback() { }

        public Feedback(Gebruiker auteur, string content, DateTime tijd, int score)
        {
            Auteur = auteur;
            Tekst = content;
            TimeWritten = tijd;
            Score = score;
        }
        #endregion
    }
}