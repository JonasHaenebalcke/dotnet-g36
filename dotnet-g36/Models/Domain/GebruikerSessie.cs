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
        private Guid _userID;
        private string _userName;
        private int _sessieID;
        #endregion

        #region properties
        public Guid UserID
        {
            get { return _userID; }
            set
            {
                _userID = User.Id;
            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = User.UserName;
            }
        }
        public Gebruiker User { get; set; }

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
        public GebruikerSessie()
        {

        }

        public GebruikerSessie(Sessie sessie, Gebruiker user)
        {
            this.Sessie = sessie;
            this.User = user;
            Aanwezig = false;
        }
        #endregion
    }
}