using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;

namespace dotnet_g36
{
    public class Verantwoordelijke : Gebruiker
    {
        #region properties
        public ICollection<Sessie> OpenTeZettenSessies { get; set; }
        public bool IsHoofdverantwoordelijke { get; set; }
        #endregion

        #region constructors
        //public Verantwoordelijke() : base() { }
        public Verantwoordelijke(string barcode, string username, string email, string wachtwoord, string voornaam, string familienaam, List<Sessie> openTeZettenSessies, StatusGebruiker statusGebruiker = StatusGebruiker.Actief)
            : base(barcode, username, email, wachtwoord, voornaam, familienaam, statusGebruiker)
        {
            this.OpenTeZettenSessies = openTeZettenSessies;// new List<Sessie>();
            IsHoofdverantwoordelijke = false;
        }
        #endregion

        #region methods

        #endregion
    }
}