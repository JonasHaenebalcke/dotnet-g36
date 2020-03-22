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
        public String Voornaam
        {
            get { return _voorNaam; }
            set
            {
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

        public ICollection<GebruikerSessie> GebruikerSessies { get; set; }

        public string Barcode { get; set; }
        public StatusGebruiker StatusGebruiker { get; set; }

        public int AantalKeerAfwezig { get; set; }

        #endregion

        #region Constructors

        public Gebruiker() { }

        public Gebruiker(string barcode, string username, string email, string voornaam, string familienaam, StatusGebruiker statusGebruiker = StatusGebruiker.Actief)
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
        }
        #endregion

        #region methods
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
            ICollection<GebruikerSessie> gebruikerSessies = GebruikerSessies;
            foreach (GebruikerSessie gebruikerSessie in gebruikerSessies)
            {
                if (gebruikerSessie.Sessie.StartDatum >= DateTime.Now)
                    gebruikerSessie.Sessie.SchrijfUit(this);
            }
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
                 if (gebruikerSessie.Sessie.Equals(sessie)  && gebruikerSessie.Aanwezig) { 
                    aanwezig = true;
                    break;
                }
            }
            return aanwezig;
        }
        #endregion
    }
}