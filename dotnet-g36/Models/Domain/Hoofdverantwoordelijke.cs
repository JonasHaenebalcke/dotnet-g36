using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Models.Domain;

namespace dotnet_g36
{
    public class Hoofdverantwoordelijke : User
    {
        #region Properties
        public IEnumerable<Sessie> OpenTeZettenSessies { get; set; }
        #endregion

        #region Constructors
        public Hoofdverantwoordelijke(string voornaam, string familienaam,StatusGebruiker statusGebruiker)
          : base(voornaam, familienaam, statusGebruiker)
        {
            // denk niet dat dit zo moet, er gaan sessies in DB zitten
            // maar voor nu heb ik het zo gedaan.
            OpenTeZettenSessies = new List<Sessie>();
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