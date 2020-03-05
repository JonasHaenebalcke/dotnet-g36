using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;

namespace dotnet_g36
{
    public class Hoofdverantwoordelijke : Verantwoordelijke
    {
        #region Properties
        #endregion

        #region Constructors
        public Hoofdverantwoordelijke(string voornaam, string familienaam,StatusGebruiker statusGebruiker, List<Sessie> sessies)
          : base(voornaam, familienaam, statusGebruiker, sessies)
        {
        }
        #endregion

        #region Methods

        #endregion
    }
}