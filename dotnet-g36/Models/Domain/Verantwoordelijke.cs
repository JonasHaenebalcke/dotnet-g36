using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;

namespace dotnet_g36
{
    public class Verantwoordelijke : User
    {
        #region properties
        public ICollection<Sessie> OpenTeZettenSessies { get; set; }
        public bool IsHoofdverantwoordelijke { get; set; }
        #endregion

        #region constructors
        public Verantwoordelijke() : base() { }
        public Verantwoordelijke(string voornaam, string familienaam, StatusGebruiker statusGebruiker, List<Sessie> openTeZettenSessies)
            : base(voornaam, familienaam, statusGebruiker)
        {
            OpenTeZettenSessies = openTeZettenSessies;
            IsHoofdverantwoordelijke = false;
        }
        #endregion

        #region methods

        #endregion
    }
}