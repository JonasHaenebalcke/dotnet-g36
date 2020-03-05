using dotnet_g36.Data;
using dotnet_g36.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36.Models.Domain
{
    public class UserSessie
    {
        #region properties
        public int UserID { get; set; }
        public User User { get; set; }

        public int SessieID { get; set; }
        public Sessie Sessie { get; set; }

        public bool Aanwezig
        {
            get => default;
            set
            {
            }
        }

        public bool Ingeschreven
        {
            get => default;
            set
            {
            }
        }
        #endregion

        #region

        public UserSessie()
        {

        }

        public UserSessie(Sessie sessie, User user)
        {
            this.SessieID = sessie.SessieID;
            this.Sessie = sessie;
            this.UserID = user.UserID;
            this.User = user;

            Ingeschreven = false;
            Aanwezig = false;
        }
        #endregion
    }
}