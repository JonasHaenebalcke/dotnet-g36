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
        public ICollection<Feedback> FeedbackList { get; set; }
        public ICollection<UserSessie> UserSessies { get; set; }
        //public Verantwoordelijke Hoofdverantwoordelijke { get; set; }
        public Verantwoordelijke Verantwoordelijke { get; set; }
        public StatusSessie StatusSessie { get; set; }

        #endregion

        #region Constructors
        public Sessie() { }

        public Sessie(/*Verantwoordelijke hoofdVerantwoordelijke,*/ Verantwoordelijke verantwoordelijke,
            string titel, string lokaal, DateTime startDatum, DateTime eindDatum, int aantalOpenPlaatsen, StatusSessie statusSessie = StatusSessie.NietOpen,
            string beschrijving = "", string gastspreker = "")
        {
            //if (verantwoordelijke == null)
            //    this.Verantwoordelijke = hoofdVerantwoordelijke;
            //else
            //    this.Verantwoordelijke = verantwoordelijke;
            //this.Verantwoordelijke = verantwoordelijke == null ? hoofdVerantwoordelijke : verantwoordelijke;
            //this.Hoofdverantwoordelijke = hoofdVerantwoordelijke;
            this.Verantwoordelijke = verantwoordelijke;
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
            if (StatusSessie.Equals(StatusSessie.NietOpen) && DateTime.Now >= StartDatum.AddHours(-1) && DateTime.Now < StartDatum)
            {
                if (!(user.IsHoofdverantwoordelijke || user.OpenTeZettenSessies.Contains(this)))
                    throw new SessieException("Sessie kan niet worden opengezet. Controleer of U de rechten hebt om deze sessie open te zetten.");
                StatusSessie = StatusSessie.Open;
            }
            else
            {
                throw new SessieException("Sessie kan niet worden opengezet. Controleer of U niet meer dan één uur op voorhand deze sessie wilt openzetten");
            }
        }

        /// <summary>
        /// Sluit Sessie en controleert op users die 3 keer afwezig waren en blokkeert deze
        /// </summary>
        public void SessieSluiten()
        {
            if (StatusSessie.Equals(StatusSessie.Open))
            {
                StatusSessie = StatusSessie.Gesloten;
                //controleert op users die 3 keer afwezig waren en blokkeert deze
                foreach (UserSessie userSessie in UserSessies)
                {
                    if (!userSessie.Aanwezig)
                    {
                        Gebruiker gebruiker = userSessie.User;
                        if (!(gebruiker is Verantwoordelijke) && gebruiker.AantalKeerAfwezig >= 2) //Verantwoordelijke niet blokkeren
                        {
                            gebruiker.StatusGebruiker = StatusGebruiker.Geblokkeerd;
                            gebruiker.SchrijfUitAlleSessies();
                        }
                        gebruiker.AantalKeerAfwezig++;
                    }
                }
            }
            else
            {
                throw new SessieException("Sessie kan niet gesloten worden.");
            }
        }

        /// <summary>
        /// De user wordt aanwezig gemeld
        /// </summary>
        /// <param name="sessie">User Object</param>
        public void MeldAanwezig(Gebruiker user)
        {
            Boolean succes = false;
            foreach (UserSessie userSessie in UserSessies)
            {
                if (userSessie.UserID == user.Id)
                //if (userSessie.UserName == user.UserName)
                {
                    if (userSessie.Aanwezig == true)
                    {
                        throw new IngeschrevenException("U bent reeds aanwezig.");
                    }
                    else
                    {
                        userSessie.Aanwezig = true;
                        succes = true;
                        break;
                    }
                }
            }
            if (!succes)
            {
                throw new IngeschrevenException("U bent niet ingeschreven, dus U kan zich niet aanwezig zetten.");
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
                if (userSessie.UserID == user.Id)
                    //if (userSessie.UserName == user.UserName)
                    throw new IngeschrevenException("U bent al ingeschreven voor deze sessie.");
            }
            if (user.StatusGebruiker == StatusGebruiker.Actief)
            {
                UserSessie usersessie = new UserSessie(this, user);
                user.UserSessies.Add(usersessie);
                UserSessies.Add(usersessie);
                if (!(user is Verantwoordelijke))
                    AantalOpenPlaatsen--;
            }
            else
            {
                throw new GeenActieveGebruikerException("U kan zich niet inschrijven omdat u geen actieve gebruiker bent. Gelieve contact op te nemen met de hoofdverantwoordelijk.");
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
                    if (!(user is Verantwoordelijke))
                        AantalOpenPlaatsen++;
                    succes = true;
                    break;
                }
            }
            if (!succes)
                throw new IngeschrevenException("U kon niet worden uitgeschreven, omdat u niet ingeschreven bent.");
        }

        /// <summary>
        /// geeft alle usernames van de aanwezigen
        /// </summary>
        /// <returns>List van string</returns>
        public List<Guid> geefAlleAanwezigen()
        {
            List<Guid> res = new List<Guid>();
            foreach (UserSessie userSessie in UserSessies)
            {
                if (userSessie.Aanwezig)
                {

                    res.Add(userSessie.UserID);


                }
            }
            return res;
        }

        /// <summary>
        /// Feedback geven op afgelopen sessies
        /// </summary>
        public void FeedbackGeven(Feedback feedback, Gebruiker gebruiker)
        {
            //Ook controleren op ingeschreven? Lijkt me overbodig maar stond wel in commentaar bij methode
            if (gebruiker.Aanwezig(this))
            {

                FeedbackList.Add(feedback);

            }
            else
            {
                throw new AanwezigException("Gebruiker was niet aanwezig! ");
            }
        }
        #endregion
    }
}