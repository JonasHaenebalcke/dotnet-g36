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

        public ICollection<UserSessie> UserSessies { get; set; }

        public int UserID { get; set; }

        public string GebruikersNaam { get; set; }

        public string Wachtwoord { get; set; }

        public StatusGebruiker StatusGebruiker { get; set; }
        #endregion

        #region Constructors


        public User(string voornaam, string familienaam,  StatusGebruiker statusGebruiker)
        {
            this.Voornaam = voornaam;
            this.Familienaam = familienaam;
          //  this.UserID = userID;
            this.StatusGebruiker = statusGebruiker;
            this.FeedbackList = new List<Feedback>();
            this.UserSessies = new List<UserSessie>();
        }
        #endregion
        #region
        public void FeedbackGeven()
        {
            // kan alleen als ingeschreven en aanwezig was
            throw new System.NotImplementedException();
        }
        #endregion
    }
}