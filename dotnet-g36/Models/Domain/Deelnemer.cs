using dotnet_g36.Models.Exceptions;
using System;
namespace dotnet_g36.Models.Domain
{
    public class Deelnemer : User
    {
        #region constructors
        public Deelnemer(string voornaam, string familienaam, StatusGebruiker statusGebruiker) : base(voornaam, familienaam, statusGebruiker)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// De gebruiker wordt aanwezig gemeld
        /// </summary>
        /// <param name="sessie">Sessie Object</param>
        public void MeldAanwezig(Sessie sessie)
        {
            foreach (UserSessie userSessie in UserSessies)
            {
                if (userSessie.SessieID == sessie.SessieID)
                {
                    userSessie.Aanwezig = true;
                }
                else
                {
                    throw new NietIngeschrevenException("U bent niet ingeschreven, dus U kan zich niet aanwezig zetten.");
                }
            }
        
        }

        /// <summary>
        /// User wordt ingeschreven bij de gekozen sessie
        /// </summary>
        /// <param name="sessie">Sessie Object</param>
        public void SchrijfIn(Sessie sessie)
        {
            foreach (UserSessie userSessie in sessie.UserSessies)
            {
                if (userSessie.UserID == UserID)
                        throw new AlIngeschrevenException("U bent al ingeschreven voor deze sessie.");                
                else
                {
                    if (StatusGebruiker == StatusGebruiker.Actief)
                    {
                        UserSessie usersessie = new UserSessie(sessie, this);
                        sessie.UserSessies.Add(usersessie);
                        UserSessies.Add(usersessie);
                        break;
                    }
                    else
                    {
                        throw new GeenActieveGebruikerException("U kan zich niet inschrijven omdat u bent geen actieve gebruiker. Glieve contact op te nemen met de hoofdverantwoordelijk.");
                    }
                }
            }
        }

        /// <summary>
        /// User wordt uitgeschreven bij de gekozen sessie
        /// </summary>
        /// <param name="sessie">sessie object</param>
        public void SchrijfUit(Sessie sessie)
        {
            bool succes = false;
                foreach (UserSessie userSessie in UserSessies)
                {
                    if (userSessie.SessieID == sessie.SessieID)
                    {
                        sessie.UserSessies.Remove(userSessie);
                        UserSessies.Remove(userSessie);
                    succes = true;
                        break;
                    }
                }
            if (!succes)
                throw new NietIngeschrevenException("Deelnemer kon niet worden uitegeschreven.");
        }

        #endregion
    }
}

