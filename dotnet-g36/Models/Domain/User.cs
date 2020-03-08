using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;


namespace dotnet_g36
{
    public class User
    {
        #region fields
        private string _familieNaam;
        private string _voorNaam;
        #endregion

        #region properties
        public String Voornaam
        {
            get { return _voorNaam; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("must have a name");
                _voorNaam = value;
            }
        }

        public String Familienaam
        {
            get { return _familieNaam; }
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

        public int AantalKeerAfwezig { get; set; }
        #endregion

        #region Constructors

        public User()
        {

        }
        public User(string voornaam, string familienaam,  StatusGebruiker statusGebruiker = StatusGebruiker.Actief)
        {
            this.Voornaam = voornaam;
            this.Familienaam = familienaam;
            this.StatusGebruiker = statusGebruiker;
            this.UserSessies = new List<UserSessie>();
            AantalKeerAfwezig = 0;
        }
        #endregion

        #region methods
        /// <summary>
        /// schrijft user uit alle sessies
        /// </summary>
        public void SchrijfUitAlleSessies() //vb indien 3 keer niet aanwezig
        {
            ICollection<UserSessie> userSessies = UserSessies;
            foreach(UserSessie userSessie in userSessies)
            {
                if (userSessie.Sessie.StartDatum >= DateTime.Now) // kan zijn dat dit niet werkt, je verandert list waarover je itereert
                {
                    userSessie.Sessie.SchrijfUit(this);
                }
            }
        }
        public void FeedbackGeven()
        {
            // kan alleen als ingeschreven en aanwezig was
            throw new System.NotImplementedException();
        }
        #endregion
    }
}