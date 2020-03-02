using System;
namespace dotnet_g36.Models.Domain
{
    public class Deelnemer : User
    {
        #region constructors
        public Deelnemer() :base()
        {
        }

        public Deelnemer(string voornaam, string familienaam, /*int userID,*/ StatusGebruiker statusGebruiker) : base(voornaam, familienaam,/* userID,*/ statusGebruiker)
        {
        }
        #endregion
    }
}

