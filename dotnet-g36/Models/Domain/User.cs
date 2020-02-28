using dotnet_g36.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;


namespace dotnet_g36
{
    public abstract class User
    {
        #region fields
        private string _familieNaam;
        private string _voorNaam;
        #endregion

        #region properties
        protected User()
        {
            _voorNaam = "tester";
            _familieNaam = "not yet given";
            StatusGebruiker = StatusGebruiker.Actief;
        }

        public User(string voornaam, string familienaam, int userID, StatusGebruiker statusGebruiker)
        {
            this.Voornaam = voornaam;
            this.Familienaam = familienaam;
            this.UserID = userID;
            this.StatusGebruiker = statusGebruiker;
        }

        public String Voornaam
        { 
            get => default; 
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("must have a name");
                _voorNaam = value;
            }
        }

        public String Familienaam
        {
            get => default;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("must have a name");
                _familieNaam = value;
            }
        }

        public IEnumerable<UserSessie> UserSessies { get; set; }

        public int UserID { get; set; }

        public string GebruikersNaam { get; set; }

        public string Wachtwoord { get; set; }

        public StatusGebruiker StatusGebruiker { get; set; }
        
        public IEnumerable<Feedback> FeedbackList { get; set; }
        #endregion  

    }
}