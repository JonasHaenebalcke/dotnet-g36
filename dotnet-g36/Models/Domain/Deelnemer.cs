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
            //Boolean us = UserSessies.Where(s => s.UserID.Equals(userID)).FirstOrDefault().Ingeschreven();
            //user = (User) UserSessies.Select(s => s.User).Where(s => s.UserID.Equals(userID));

            //// Als gebruiker is ingeschreven en niet in de lijst van aanwezigen zit, steek in lijst aanwezigen en return true;

            foreach (UserSessie userSessie in UserSessies)
            {
                if (userSessie.SessieID == sessie.SessieID && userSessie.Ingeschreven)
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
                {
                    if (!userSessie.Ingeschreven)
                    {
                        if (StatusGebruiker == StatusGebruiker.Actief)
                        {
                            userSessie.Ingeschreven = true;
                        }
                        else
                        {
                            throw new GeenActieveGebruikerException("U kan zich niet inschrijven omdat u bent geen actieve gebruiker. Glieve contact op te nemen met de hoofdverantwoordelijk.");
                        }
                    } else
                        throw new AlIngeschrevenException("U bent al ingeschreven voor deze sessie.");
                }
                else
                {
                    if (StatusGebruiker == StatusGebruiker.Actief)
                    {
                        UserSessie usersessie = new UserSessie(sessie, this);
                        sessie.UserSessies.Add(usersessie);
                        UserSessies.Add(usersessie);
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
            foreach (UserSessie userSessie in UserSessies)
            {
                if (userSessie.SessieID == sessie.SessieID &&userSessie.Ingeschreven)
                {
                    userSessie.Ingeschreven = false;
                }
            }
        }

        #endregion
    }
}

