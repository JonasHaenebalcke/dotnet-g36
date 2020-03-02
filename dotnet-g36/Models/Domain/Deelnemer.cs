using System;
namespace dotnet_g36.Models.Domain
{
    public class Deelnemer : User
    {
        #region constructors
        public Deelnemer(string voornaam, string familienaam,  StatusGebruiker statusGebruiker) : base(voornaam, familienaam, statusGebruiker)
        {
        }
        #endregion

        #region Methods
        public bool MeldAanwezig(int sessieID, int userID)
        {
            //     Boolean us = UserSessies.Where(s => s.UserID.Equals(userID)).FirstOrDefault().Ingeschreven();

            //// Als gebruiker is ingeschreven en niet in de lijst van aanwezigen zit, steek in lijst aanwezigen en return true;

            return false;
            //throw new System.NotImplementedException();
        }

        public bool SchrijfIn(int sessieID, int userID)
        {
            // als gebruiker nog niet is ingeschreven dan gebruiker ingeschrijven
            // if(Ingeschrevenen.Contains() )
            throw new System.NotImplementedException();
        }

        public bool SchrijfUit(int sessieID, int userID)
        {
            // als gebruiker nog is ingeschreven dan gebruiker uitschrijven
            throw new System.NotImplementedException();
        }

        public void FeedbackGeven()
        {
            // kan alleen als ingeschreven en aanwezig was
            throw new System.NotImplementedException();
        }
        #endregion
    }
}

