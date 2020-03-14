using dotnet_g36.Models.Exceptions;
using System;
using System.Collections.Generic;

namespace dotnet_g36.Models.Domain
{
    public class Sessie
    {

        #region Properties
        public int SessieID { get; set; }
        public string Titel { get; set; }
        public string Gastspreker { get; set; }
        public string Lokaal { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public int AantalOpenPlaatsen { get; set; }
        public string Beschrijving { get; set; }
        public IEnumerable<Media> Media { get; set; }
        public IEnumerable<Feedback> FeedbackList { get; set; }
        public ICollection<UserSessie> UserSessies { get; set; }
        public Verantwoordelijke Hoofdverantwoordelijke { get; set; }
        public Verantwoordelijke Verantwoordelijke { get; set; }
        public StatusSessie StatusSessie { get; set; }

        #endregion

        #region Constructors
        public Sessie() { }

        public Sessie(Verantwoordelijke hoofdVerantwoordelijke, Verantwoordelijke verantwoordelijke,
            string titel, string lokaal, DateTime startDatum, DateTime eindDatum, int aantalOpenPlaatsen, StatusSessie statusSessie = StatusSessie.NietOpen,
            string beschrijving = "", string gastspreker = "")
        {
            this.Verantwoordelijke = verantwoordelijke == null ? hoofdVerantwoordelijke : verantwoordelijke;
            this.Hoofdverantwoordelijke = hoofdVerantwoordelijke;
            this.Titel = titel;
            this.Lokaal = lokaal;
            this.StartDatum = startDatum;
            this.EindDatum = eindDatum;
            this.AantalOpenPlaatsen = aantalOpenPlaatsen;
            this.StatusSessie = statusSessie;
            this.Beschrijving = beschrijving;
            this.Gastspreker = gastspreker;
            this.UserSessies = new List<UserSessie>();
            this.FeedbackList = new List<Feedback>();
            this.Media = new List<Media>();

        }
        #endregion

        #region methods
        /// <summary>
        /// Zet Sessie open
        /// </summary>
        /// <param name="user">Verantwoordelijke Object</param>
        public void SessieOpenZetten(Verantwoordelijke user)
        {
            if (user.OpenTeZettenSessies.Contains(this) && StatusSessie.Equals(StatusSessie.NietOpen) && DateTime.Now >= StartDatum.AddHours(-1))
            {
                StatusSessie = StatusSessie.Open;
            }
            else
            {
                throw new GeenSessiesException("Sessie kan niet worden opengezet.");
            }
        }

        /// <summary>
        /// Sluit Sessie en controleert op users die 3 keer afwezig waren en blokkeert deze
        /// </summary>
        /// <param name="user">Verantwoordelijke Object</param>
        public void SessieSluiten(Verantwoordelijke user)
        {
            if (user.OpenTeZettenSessies.Contains(this) & StatusSessie.Equals(StatusSessie.Open))
            {
                StatusSessie = StatusSessie.Gesloten;
                //controleert op users die 3 keer afwezig waren en blokkeert deze
                foreach (UserSessie userSessie in UserSessies)
                {
                    if (!userSessie.Aanwezig)
                    {
                        Gebruiker user1 = userSessie.User;
                        if (!(user1 is Verantwoordelijke) && user1.AantalKeerAfwezig >= 2) //Verantwoordelijke niet blokkeren
                        {
                            user1.StatusGebruiker = StatusGebruiker.Geblokkeerd;
                            user1.SchrijfUitAlleSessies();
                        }
                        user1.AantalKeerAfwezig++;
                    }
                }
            }
            else
            {
                throw new GeenSessiesException("Sessie kan niet gesloten worden.");
            }
        }

        /// <summary>
        /// De user wordt aanwezig gemeld
        /// </summary>
        /// <param name="sessie">User Object</param>
        public void MeldAanwezig(Gebruiker user)
        {
            foreach (UserSessie userSessie in UserSessies)
            {
                //if (userSessie.UserID == user.Id)
                if (userSessie.UserName == user.UserName)
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
        /// User wordt ingeschreven bij de sessie
        /// </summary>
        /// <param name="sessie">User Object</param>
        public void SchrijfIn(Gebruiker user)
        {
            if (StartDatum < DateTime.Now || AantalOpenPlaatsen < 1)
                throw new ArgumentException("je kan je niet inschrijven in een verleden maand.");
            foreach (UserSessie userSessie in UserSessies)
            {
                //if (userSessie.UserID == user.Id)
                if (userSessie.UserName == user.UserName)
                    throw new AlIngeschrevenException("U bent al ingeschreven voor deze sessie.");
            }
            if (user.StatusGebruiker == StatusGebruiker.Actief)
            {
                UserSessie usersessie = new UserSessie(this, user);
                user.UserSessies.Add(usersessie);
                UserSessies.Add(usersessie);
            }
            else
            {
                throw new GeenActieveGebruikerException("U kan zich niet inschrijven omdat u bent geen actieve gebruiker. Glieve contact op te nemen met de hoofdverantwoordelijk.");
            }
        }

        /// <summary>
        /// User wordt uitgeschreven bij de sessie
        /// </summary>
        /// <param name="sessie">User object</param>
        public void SchrijfUit(Gebruiker user)
        {
            if (StartDatum < DateTime.Now)
                throw new ArgumentException("je kan je niet uitschreven in een verleden maand.");

            bool succes = false;
            foreach (UserSessie userSessie in UserSessies)
            {
                if (userSessie.UserID == user.Id)
                //if (userSessie.UserName == user.UserName)
                {
                    user.UserSessies.Remove(userSessie);
                    UserSessies.Remove(userSessie);
                    succes = true;
                    break;
                }
            }
            if (!succes)
                throw new NietIngeschrevenException("Deelnemer kon niet worden uitegeschreven.");
        }

        /// <summary>
        /// geeft alle usernames van de aanwezigen
        /// </summary>
        /// <returns>List van string</returns>
        public List<string> geefAlleAanwezigen()
        {
            List<string> res = new List<string>();
            foreach (UserSessie userSessie in UserSessies)
            {
                if (userSessie.Aanwezig)
                {
                    res.Add(userSessie.User.UserName);
                }
            }
            return res;
        }
        #endregion
    }
}