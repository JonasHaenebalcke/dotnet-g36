using dotnet_g36.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;


namespace dotnet_g36
{
    public class Gebruiker : IdentityUser<Guid>
    {
        #region fields
        private string _familieNaam;
        private string _voorNaam;
        #endregion

        #region properties
        public string Voornaam
        {
            get { return _voorNaam; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("must have a name");
                _voorNaam = value;
            }
        }

        public string Familienaam
        {
            get { return _familieNaam; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("must have a name");
                _familieNaam = value;
            }
        }

        public ICollection<GebruikerSessie> GebruikerSessies { get; set; }
        public string Barcode { get; set; }
        public StatusGebruiker StatusGebruiker { get; set; }
        public int AantalKeerAfwezig { get; set; }
        public ICollection<Feedback> FeedbackList { get; set; }

        //java
        public TypeGebruiker TypeGebruiker { get; set; }
        public ICollection<Sessie> OpenTeZettenSessies { get; set; }

        public String PasswordHashJava { get; set; }

        #endregion

        #region Constructors

        public Gebruiker() { }

        public Gebruiker(string barcode, string username, string email, string voornaam, string familienaam, StatusGebruiker statusGebruiker = StatusGebruiker.Actief, TypeGebruiker typeGebruiker = TypeGebruiker.Gebruiker)
        {
            Barcode = barcode;
            Email = email;
            NormalizedEmail = email;
            AccessFailedCount = 0;
            UserName = username;
            NormalizedUserName = username;
            Voornaam = voornaam;
            Familienaam = familienaam;
            StatusGebruiker = statusGebruiker;
            GebruikerSessies = new List<GebruikerSessie>();
            AantalKeerAfwezig = 0;
            TypeGebruiker = typeGebruiker;
            OpenTeZettenSessies = new List<Sessie>();
        }
        #endregion

        #region methods
        public void AddSessieLijst(List<Sessie> sessies)
        {
            OpenTeZettenSessies = sessies;
        }

        public void AddSessie(Sessie sessie)
        {
            OpenTeZettenSessies.Add(sessie);
        }
        /// <summary>
        ///  Geeft de Volledige naam van de gebruiker
        /// </summary>
        /// <returns>string volledige naam</returns>
        public string GeefVolledigeNaam()
        {
            return Voornaam + ' ' + Familienaam;
        }

        /// <summary>
        /// Schrijft de gebruiker/user uit alle sessies
        /// </summary>
        public void SchrijfUitAlleSessies() //vb indien 3 keer niet aanwezig
        {
            bool succes = false;
            foreach (GebruikerSessie gebruikerSessie in GebruikerSessies)
            {
                if (gebruikerSessie.Sessie.StartDatum > DateTime.Now && gebruikerSessie.Sessie.StatusSessie == StatusSessie.Gesloten)
                {
                   // gebruikerSessie.Sessie.SchrijfUit(this);
                    succes = true;
                    break;
                }
            }
          //  if (succes)
              //  SchrijfUitAlleSessies();
        }

        /// <summary>
        /// Controleert of de Gebruiker aanwezig was op een sessie
        /// </summary>
        /// <param name="sessie">Sessie Object</param>
        /// <returns>bool aanwezig</returns>
        public bool Aanwezig(Sessie sessie)
        {
            bool aanwezig = false;
            foreach (GebruikerSessie gebruikerSessie in GebruikerSessies)
            {
                if (gebruikerSessie.Sessie.Equals(sessie) && gebruikerSessie.Aanwezig)
                {
                    aanwezig = true;
                    break;
                }
            }
            return aanwezig;
        }
        #endregion
    }
}