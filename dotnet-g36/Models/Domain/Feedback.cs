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

        public string AuteursNaam { get; set; }
        #endregion

        #region constructor
        public Feedback(Gebruiker auteur, string content, DateTime tijd)
        {
            this.AuteursNaam = auteur.GeefVolledigeNaam();
            this.Tekst = content;
            this.TimeWritten = tijd;
        }

        public Feedback()
        {

        }
        #endregion
    }
}