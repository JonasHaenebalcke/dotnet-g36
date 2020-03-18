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

        public Gebruiker Auteur { get; set; }
        #endregion

        #region constructor
        public Feedback(Gebruiker auteur, string content, DateTime tijd)
        {
            this.Auteur = auteur;
            this.NaamAuteur = auteur.Voornaam + " " + auteur.Familienaam;
            this.Tekst = content;
            this.TimeWritten = tijd;
        }

        public Feedback()
        {

        }
        #endregion
    }
}