using dotnet_g36.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            string titel , string lokaal, DateTime startDatum, DateTime eindDatum, int aantalOpenPlaatsen, StatusSessie statusSessie =  StatusSessie.NietOpen,
            string beschrijving = "", string gastspreker= "")   
        {
            this.Verantwoordelijke = verantwoordelijke;
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
        /// Sluit Sessie
        /// </summary>
        /// <param name="user">Verantwoordelijke Object</param>
        public void SessieSluiten(Verantwoordelijke user)
        {
            if (user.OpenTeZettenSessies.Contains(this) & StatusSessie.Equals(StatusSessie.Open))
            {
                StatusSessie = StatusSessie.Gesloten;
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
        public void MeldAanwezig(User user)
        {
            foreach (UserSessie userSessie in UserSessies)
            {
                if (userSessie.UserID == user.UserID)
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
        public void SchrijfIn(User user)
        {
            if (StartDatum < DateTime.Now || AantalOpenPlaatsen < 1)
                throw new ArgumentException("je kan je niet inschrijven in een verleden maand.");
            foreach (UserSessie userSessie in UserSessies)
            {
                if (userSessie.UserID == user.UserID)
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
        public void SchrijfUit(User user)
        {
            if (StartDatum < DateTime.Now)
                throw new ArgumentException("je kan je niet uitschreven in een verleden maand.");

            bool succes = false;
            foreach (UserSessie userSessie in UserSessies)
            {
                if (userSessie.UserID == user.UserID)
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
        #endregion
    }
}