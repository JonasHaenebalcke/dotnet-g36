using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36
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
    }
}