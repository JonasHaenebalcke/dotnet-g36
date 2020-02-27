using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Models.Domain;

namespace dotnet_g36
{
    public class Verantwoordelijke : User
    {
        public Verantwoordelijke(string voornaam, string familienaam, int userID, StatusGebruiker statusGebruiker) : base(voornaam, familienaam, userID, statusGebruiker)
        {
        }

        public List<Sessie> GeorganiseerdeSessies
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