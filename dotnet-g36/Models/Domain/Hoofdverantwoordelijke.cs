using dotnet_g36.Models.Domain;
using System.Collections.Generic;

namespace dotnet_g36
{
    public class Hoofdverantwoordelijke : Verantwoordelijke
    {
        #region Properties
        #endregion

        #region Constructors
        public Hoofdverantwoordelijke() : base() { }
        public Hoofdverantwoordelijke(string voornaam, string familienaam, StatusGebruiker statusGebruiker, List<Sessie> sessies)
          : base(voornaam, familienaam, statusGebruiker, sessies)
        {
        }
        #endregion

        #region Methods

        #endregion
    }
}