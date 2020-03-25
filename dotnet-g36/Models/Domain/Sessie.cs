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
        public ICollection<Media> Media { get; set; }
        public ICollection<Feedback> FeedbackList { get; set; }
        public ICollection<GebruikerSessie> GebruikerSessies { get; set; }
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
            this.GebruikerSessies = new List<GebruikerSessie>();
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
                throw new SessieException("Sessie kan niet worden opengezet. Controleer of je niet meer dan één uur op voorhand deze sessie wilt openzetten");
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
                foreach (GebruikerSessie gebruikerSessie in GebruikerSessies)
                {
                    if (!gebruikerSessie.Aanwezig)
                    {
                        Gebruiker gebruiker = gebruikerSessie.Gebruiker;
                        if (!(gebruiker is Verantwoordelijke) && gebruiker.AantalKeerAfwezig >= 2) //Verantwoordelijken niet blokkeren
                        {
                            gebruiker.StatusGebruiker = StatusGebruiker.Geblokkeerd;
                            gebruiker.SchrijfUitAlleSessies();
                        }
                        gebruiker.AantalKeerAfwezig++;
                        break;
                    }
                }
            }
            else
                throw new SessieException("Sessie kan niet gesloten worden.");
        }

        /// <summary>
        /// De gebruiker wordt aanwezig gemeld
        /// </summary>
        /// <param name="sessie">Gebruiker Object</param>
        public void MeldAanwezigAfwezig(Gebruiker gebruiker)
        {
            if (gebruiker.StatusGebruiker != StatusGebruiker.Actief)
                throw new GeenActieveGebruikerException("Gebruiker is niet actief.");

            bool succes = false;
            foreach (GebruikerSessie gebruikerSessie in GebruikerSessies)
            {
                if (gebruikerSessie.Gebruiker == gebruiker)
                {
                    if (gebruikerSessie.Aanwezig == true)
                    {
                        gebruikerSessie.Aanwezig = false;
                        succes = true;
                    }
                    else
                    {
                        gebruikerSessie.Aanwezig = true;
                        succes = true;
                        
                    }
                    break;
                }
            }
            if (!succes)
            {
                throw new SchrijfInSchrijfUitException("Je bent niet ingeschreven, dus je kan zich niet aanwezig zetten.");
            }
        }


      

        /// <summary>
        /// User wordt ingeschreven bij de sessie
        /// </summary>
        /// <param name="sessie">User Object</param>
        public void SchrijfIn(Gebruiker gebruiker)
        {
            if (StartDatum < DateTime.Now)
                throw new SchrijfInSchrijfUitException("je kan je niet inschrijven in een verleden maand.");
            if (GebruikerSessies.Count >= Capaciteit)
                throw new SchrijfInSchrijfUitException("je kan je niet meer inschrijven in deze sessie. De sessie is volzet.");

            foreach (GebruikerSessie gebruikerSessie in GebruikerSessies)
            {
                if (gebruikerSessie.Gebruiker == gebruiker)
                    throw new SchrijfInSchrijfUitException("Je bent al ingeschreven voor deze sessie.");
            }

            if (gebruiker.StatusGebruiker == StatusGebruiker.Actief)
            {
                GebruikerSessie gebruikersessie = new GebruikerSessie(this, gebruiker);
                gebruiker.GebruikerSessies.Add(gebruikersessie);
                GebruikerSessies.Add(gebruikersessie);
            }
            else
                throw new GeenActieveGebruikerException("Je kan zich niet inschrijven omdat je geen actieve gebruiker bent. Gelieve contact op te nemen met de hoofdverantwoordelijk.");
        }

        /// <summary>
        /// User wordt uitgeschreven bij de sessie
        /// </summary>
        /// <param name="sessie">User object</param>
        public void SchrijfUit(Gebruiker gebruiker)
        {
            if (StartDatum < DateTime.Now)
                throw new SchrijfInSchrijfUitException("Je kan je niet uitschreven in een verleden maand.");

            if (gebruiker == Verantwoordelijke)
                throw new SchrijfInSchrijfUitException("Je kan je niet uitschreven voor een sessie waarvoor je verantwoordelijk bent.");

            bool succes = false;
            foreach (GebruikerSessie gebruikerSessie in GebruikerSessies)
            {
                if (gebruikerSessie.Gebruiker == gebruiker)
                {
                    gebruiker.GebruikerSessies.Remove(gebruikerSessie);
                    GebruikerSessies.Remove(gebruikerSessie);
                    succes = true;
                    break;
                }
            }
            if (!succes)
                throw new SchrijfInSchrijfUitException("Je kon niet worden uitgeschreven, omdat je niet ingeschreven bent.");
        }

        /// <summary>
        /// geeft alle usernames van de aanwezigen
        /// </summary>
        /// <returns>List van string</returns>
        public List<Gebruiker> geefAlleAanwezigen()
        {
            List<Gebruiker> res = new List<Gebruiker>();
            foreach (GebruikerSessie gebruikerSessie in GebruikerSessies)
            {
                if (gebruikerSessie.Aanwezig)
                    res.Add(gebruikerSessie.Gebruiker);
            }
            return res;
        }
        public List<string> geefAlleIngeschrevenenNamen()
        {
            List<string> temp = new List<string>();
            foreach (GebruikerSessie gebruikerSessie in GebruikerSessies)
            {   
                temp.Add(gebruikerSessie.Gebruiker.GeefVolledigeNaam());
            }
            return temp;
        }

        /// <summary>
        /// Feedback geven op de afgelopen sessie
        /// </summary>
        /// <param name="feedbacktxt">feedback tekst</param>
        /// <param name="gebruiker">Gebruiker Object</param>
        public void FeedbackGeven(string feedbacktxt, Gebruiker gebruiker, int score)
        {
            
            if (StatusSessie != StatusSessie.Gesloten)
                throw new FeedbackException("Je kan geen feedback geven op een niet afgelopen sessie.");
            if (gebruiker.StatusGebruiker != StatusGebruiker.Actief)
                throw new GeenActieveGebruikerException("Je moet een actieve gebruiker zijn om feedback te kunnen geven");
            if (score < 1 || score > 5)
                throw new FeedbackException("Score moet tussen 1 en 5 liggen");

            foreach (Feedback f in FeedbackList)
            {
                if (f.Auteur == gebruiker)
                    throw new FeedbackException("Gebruiker heeft al feedback gegeven.");
            }

            if (gebruiker.Aanwezig(this))
            {
                Feedback feedback = new Feedback(gebruiker, feedbacktxt, DateTime.Now, score);
                FeedbackList.Add(feedback);
            }
            else
                throw new AanwezigException("Gebruiker was niet aanwezig of niet ingeschreven en kan dus geen feedback geven!");
        }

        /// <summary>
        /// Verwijderd de meegeven feedback
        /// </summary>
        /// <param name="feedbackId">int feedbackId</param>
        /// <param name="gebruiker">Gebruiker object</param>
        public void VerwijderFeedback(int feedbackId, Gebruiker gebruiker)
        {
            bool succes = false;
            foreach (Feedback feedback in FeedbackList)
            {
                if (feedback.FeedbackID == feedbackId)
                {
                    if (gebruiker == feedback.Auteur || (gebruiker is Verantwoordelijke && (gebruiker as Verantwoordelijke).IsHoofdverantwoordelijke == true))
                    {
                        succes = true;
                        FeedbackList.Remove(feedback);
                        break;
                    }
                    else
                        throw new FeedbackException("Feedback is niet verwijderd. Controleer of je de auteur bent van de gekozen feedback of de juiste rechten hebt.");
                }
            }
            if (!succes)
                throw new FeedbackException("Feedback kon niet gevonden worden. Feedback is niet verwijderd.");
        }

        #endregion
    }
}