using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36
{
    public class Hoofdverantwoordelijke : User
    {
        public List<Sessie> AlleSessies
        {
            get => default;
            set
            {
            }
        }

        public void SessieOpenZetten(int sessieID)
        {
            throw new System.NotImplementedException();
        }

        public void SessieSluiten(int sessieID)
        {
            throw new System.NotImplementedException();
        }
    }
}