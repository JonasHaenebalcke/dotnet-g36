using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Models.Domain;

namespace dotnet_g36
{
    public class Verantwoordelijke : User
    {
        #region properties
        public IEnumerable<Sessie> GeorganiseerdeSessies { get; set; }
        #endregion

        #region constructors
        public Verantwoordelijke(string voornaam, string familienaam, /*int userID,*/ StatusGebruiker statusGebruiker)
            : base(voornaam, familienaam,/* userID,*/ statusGebruiker)
        {
            GeorganiseerdeSessies = new List<Sessie>();
        }
        #endregion

        
        #region methods
        public bool SessieOpenZetten(int sessieID)
        {
            throw new System.NotImplementedException();
        }

        public bool SessieSluiten(int sessieID)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}