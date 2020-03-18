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
        public int Capaciteit { get; set; }
        public string Beschrijving { get; set; }
        public IEnumerable<Media> Media { get; set; }
        public IEnumerable<Feedback> FeedbackList { get; set; }
        public ICollection<UserSessie> UserSessies { get; set; }
        //public Verantwoordelijke Hoofdverantwoordelijke { get; set; }
        public Verantwoordelijke Verantwoordelijke { get; set; }
        public StatusSessie StatusSessie { get; set; }

        #endregion

        #region Constructors
        public Sessie() { }

        public Sessie(Verantwoordelijke verantwoordelijke,
            string titel, string lokaal, DateTime startDatum, DateTime eindDatum, int capaciteit, StatusSessie statusSessie = StatusSessie.NietOpen,
            string beschrijving = "", string gastspreker = "")
        {
            this.Verantwoordelijke = verantwoordelijke;
            this.Titel = titel;
            this.Lokaal = lokaal;
            this.StartDatum = startDatum;
            this.EindDatum = eindDatum;
            this.Capaciteit = capaciteit;
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
                    throw new SessieException("Sessie kan niet worden opengezet. Controleer of je de rechten hebt om deze sessie open te zetten.");
                StatusSessie = StatusSessie.Open;
            }
            else
            {
                throw new SessieException("Sessie kan niet worden opengezet. Controleer of je niet meer dan één uur op voorhand deze sessie wilt openzetten");
            }
        }

        /// <summary>
        /// Sluit Sessie en controleert op users die 3 keer afwezig waren en blokkeert deze
        /// </summary>
        public void SessieSluiten(ICollection<Gebruiker> gebruikers)
        {
            if (StatusSessie.Equals(StatusSessie.Open))
            {
                StatusSessie = StatusSessie.Gesloten;
                //controleert op users die 3 keer afwezig waren en blokkeert deze
                foreach (UserSessie userSessie in UserSessies)
                {
                    if (!userSessie.Aanwezig)
                    {
                        foreach(Gebruiker g in gebruikers)
                        {
                            if (g.Id == userSessie.UserID)
                            {
                                //Gebruiker gebruiker = userSessie.User;
                                if (!(g is Verantwoordelijke) && g.AantalKeerAfwezig >= 2) //Verantwoordelijke niet blokkeren
                                {
                                    g.StatusGebruiker = StatusGebruiker.Geblokkeerd;
                                    g.SchrijfUitAlleSessies();
                                }
                                g.AantalKeerAfwezig++;
                                break;
                            }
                        }
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
                        throw new IngeschrevenException("Je bent reeds aanwezig.");
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
                throw new IngeschrevenException("Je bent niet ingeschreven, dus je kan zich niet aanwezig zetten.");
            }
        }

        /// <summary>
        /// User wordt ingeschreven bij de sessie
        /// </summary>
        /// <param name="sessie">User Object</param>
        public void SchrijfIn(Gebruiker user)
        {
            if (StartDatum < DateTime.Now || Capaciteit < 1)
                throw new ArgumentException("je kan je niet inschrijven in een verleden maand.");
            foreach (UserSessie userSessie in UserSessies)
            {
                if (userSessie.UserID == user.Id)
                    throw new IngeschrevenException("Je bent al ingeschreven voor deze sessie.");
            }
            if (user.StatusGebruiker == StatusGebruiker.Actief)
            {
                UserSessie usersessie = new UserSessie(this, user);
                user.UserSessies.Add(usersessie);
                UserSessies.Add(usersessie);
            }
            else
            {
                throw new GeenActieveGebruikerException("Je kan zich niet inschrijven omdat je geen actieve gebruiker bent. Gelieve contact op te nemen met de hoofdverantwoordelijk.");
            }
        }

        /// <summary>
        /// User wordt uitgeschreven bij de sessie
        /// </summary>
        /// <param name="sessie">User object</param>
        public void SchrijfUit(Gebruiker user)
        {
            if (StartDatum < DateTime.Now)
                throw new ArgumentException("Je kan je niet uitschreven in een verleden maand.");

            if (user == Verantwoordelijke)
                throw new IngeschrevenException("Je kan je niet uitschreven voor een sessie waarvoor je verantwoordelijk bent.");

            bool succes = false;
            foreach (UserSessie userSessie in UserSessies)
            {
                if (userSessie.UserID == user.Id)
                //if (userSessie.UserName == user.UserName)
                {
                    user.UserSessies.Remove(userSessie);
                    UserSessies.Remove(userSessie);
                    if (!(user is Verantwoordelijke))
                        Capaciteit++;
                    succes = true;
                    break;
                }
            }
            if (!succes)
                throw new IngeschrevenException("Je kon niet worden uitgeschreven, omdat je niet ingeschreven bent.");
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
        #endregion
    }
}