using dotnet_g36.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;


namespace dotnet_g36
{
    public class User
    {
        private string _familieNaam;
        private string _voorNaam;

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

        public List<UserSessie> UserSessies { get; set; }

        public int UserID { get; set; }

        public String GebruikersNaam { get; set; }

        public String Wachtwoord { get; set; }

        public StatusGebruiker StatusGebruiker { get; set; }

        public List<Feedback> FeedbackList { get; set; }

    }
}