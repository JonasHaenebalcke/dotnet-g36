using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Models.Domain;

namespace dotnet_g36
{
    public class Verantwoordelijke : User
    {
        #region constructors
        public Verantwoordelijke(string voornaam, string familienaam, int userID, StatusGebruiker statusGebruiker) : base(voornaam, familienaam, userID, statusGebruiker)
        {
        }
        #endregion

        #region properties
        public List<Sessie> GeorganiseerdeSessies
        {
            get => default;
            set
            {
            }
        }
        #endregion

        #region methods
        public void SessieOpenZetten(int sessieID)
        {
            throw new System.NotImplementedException();
        }

        public void SessieSluiten(int sessieID)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}