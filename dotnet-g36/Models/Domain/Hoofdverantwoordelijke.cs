using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Models.Domain;

namespace dotnet_g36
{
    public class Hoofdverantwoordelijke : User
    {
        #region Properties
        public IEnumerable<Sessie> AlleSessies { get; set; }
        #endregion

        #region Constructors
        public Hoofdverantwoordelijke(string voornaam, string familienaam, int userID, StatusGebruiker statusGebruiker)
          : base(voornaam, familienaam, userID, statusGebruiker)
        {
            // denk niet dat dit zo moet, er gaan sessies in DB zitten
            // maar voor nu heb ik het zo gedaan.
            AlleSessies = new List<Sessie>();
        }
        #endregion

        #region Methods
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