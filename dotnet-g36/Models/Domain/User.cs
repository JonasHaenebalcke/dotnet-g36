using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;


namespace dotnet_g36
{
    public class User
    {
        #region fields
        private string _familieNaam;
        private string _voorNaam;
        #endregion


        #region properties


        public String Voornaam
        {
            get { return _voorNaam; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("must have a name");
                _voorNaam = value;
            }
        }

        public String Familienaam
        {
            get { return _familieNaam; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("must have a name");
                _familieNaam = value;
            }
        }

        public ICollection<UserSessie> UserSessies { get; set; }

        public int UserID { get; set; }

        public string GebruikersNaam { get; set; }

        public string Wachtwoord { get; set; }

        public StatusGebruiker StatusGebruiker { get; set; }
        #endregion

        #region Constructors

        /*public User()
        {

        }*/
        public User(string voornaam, string familienaam,  StatusGebruiker statusGebruiker = StatusGebruiker.Actief)
        {
            this.Voornaam = voornaam;
            this.Familienaam = familienaam;
            this.StatusGebruiker = statusGebruiker;
            this.UserSessies = new List<UserSessie>();
        }
        #endregion
        #region methods
        ///// <summary>
        ///// De gebruiker wordt aanwezig gemeld
        ///// </summary>
        ///// <param name="sessie">Sessie Object</param>
        //public void MeldAanwezig(Sessie sessie)
        //{
        //    foreach (UserSessie userSessie in UserSessies)
        //    {
        //        if (userSessie.SessieID == sessie.SessieID)
        //        {
        //            userSessie.Aanwezig = true;
        //        }
        //        else
        //        {
        //            throw new NietIngeschrevenException("U bent niet ingeschreven, dus U kan zich niet aanwezig zetten.");
        //        }
        //    }

        //}

        ///// <summary>
        ///// User wordt ingeschreven bij de gekozen sessie
        ///// </summary>
        ///// <param name="sessie">Sessie Object</param>
        //public void SchrijfIn(Sessie sessie)
        //{
        //    foreach (UserSessie userSessie in sessie.UserSessies)
        //    {
        //        if (userSessie.UserID == UserID)
        //            throw new AlIngeschrevenException("U bent al ingeschreven voor deze sessie.");
        //        else
        //        {
        //            if (StatusGebruiker == StatusGebruiker.Actief)
        //            {
        //                UserSessie usersessie = new UserSessie(sessie, this);
        //                sessie.UserSessies.Add(usersessie);
        //                UserSessies.Add(usersessie);
        //                break;
        //            }
        //            else
        //            {
        //                throw new GeenActieveGebruikerException("U kan zich niet inschrijven omdat u bent geen actieve gebruiker. Glieve contact op te nemen met de hoofdverantwoordelijk.");
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// User wordt uitgeschreven bij de gekozen sessie
        ///// </summary>
        ///// <param name="sessie">sessie object</param>
        //public void SchrijfUit(Sessie sessie)
        //{
        //    bool succes = false;
        //    foreach (UserSessie userSessie in UserSessies)
        //    {
        //        if (userSessie.SessieID == sessie.SessieID)
        //        {
        //            sessie.UserSessies.Remove(userSessie);
        //            UserSessies.Remove(userSessie);
        //            succes = true;
        //            break;
        //        }
        //    }
        //    if (!succes)
        //        throw new NietIngeschrevenException("Deelnemer kon niet worden uitegeschreven.");
        //}
        public void FeedbackGeven()
        {
            // kan alleen als ingeschreven en aanwezig was
            throw new System.NotImplementedException();
        }
        #endregion
    }
}