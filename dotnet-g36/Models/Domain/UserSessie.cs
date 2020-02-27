using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36
{
    public class UserSessie
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int SessieID { get; set; }
        public Sessie Sessie { get; set; }
    }
}