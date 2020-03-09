﻿using dotnet_g36.Data;
using dotnet_g36.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36.Models.Domain
{
    public class UserSessie
    {

        #region Fields
        private string _userID;
        private int _sessieID; 
        #endregion

        #region properties
        public string UserID
        {
            get { return _userID; }
            set
            {
                _userID = User.Id;
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

        public bool Aanwezig
        {
            get => default;
            set
            {
            }
        }
        #endregion

        #region Constructors
        public UserSessie()
        {

        }

        public UserSessie(Sessie sessie, Gebruiker user)
        {
          //  this.SessieID = sessie.SessieID;
            this.Sessie = sessie;
            //this.UserID = user.UserID;
            this.User = user;
            Aanwezig = false;
        }
        #endregion
    }
}