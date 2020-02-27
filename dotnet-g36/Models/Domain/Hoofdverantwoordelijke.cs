using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Models.Domain;

namespace dotnet_g36
{
    public class Hoofdverantwoordelijke : User
    {
        public Hoofdverantwoordelijke(string voornaam, string familienaam, int userID, StatusGebruiker statusGebruiker) : base(voornaam, familienaam, userID, statusGebruiker)
        {
        }

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