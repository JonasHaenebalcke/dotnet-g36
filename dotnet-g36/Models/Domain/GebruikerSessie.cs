using dotnet_g36.Data;
using dotnet_g36.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36.Models.Domain
{
    public class GebruikerSessie
    {
        #region Fields
        private Guid _GebruikerID;
        private int _sessieID;
        #endregion

        #region properties
        public Guid GebruikerID
        {
            get { return _GebruikerID; }
            set
            {
                _GebruikerID = Gebruiker.Id;
            }
        }

        public Gebruiker Gebruiker { get; set; }

        public int SessieID
        {
            get { return _sessieID; }
            set
            {
                _sessieID = Sessie.SessieID;
            }
        }
        public Sessie Sessie { get; set; }

        public bool Aanwezig { get; set; }
        #endregion

        #region Constructors
        public GebruikerSessie() { } 

        public GebruikerSessie(Sessie sessie, Gebruiker gebruiker)
        {
            this.Sessie = sessie;
            this.Gebruiker = gebruiker;
            Aanwezig = false;
        }
        #endregion
    }
}