using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnet_g36
{
    public class Sessie
    {
        #region Constructors
        public Sessie() { }
        public Sessie(int sessieID, Hoofdverantwoordelijke hoofdVerantwoordelijke, Verantwoordelijke verantwoordelijke, string titel, string gastspreker, string lokaal,
            DateTime startDatum, DateTime startUur, DateTime eindDatum, DateTime eindUur, int aantalOpenPlaatsen,
            string beschrijving, Month month, List<UserSessie> ingeschreven = null, List<Media> media = null, List<Feedback> feedback = null, List<UserSessie> aanwezigen = null)   
        {
            this.Verantwoordelijke = verantwoordelijke;
            this.Hoofdverantwoordelijke = hoofdVerantwoordelijke;
            this.SessieID= sessieID;
            this.Titel = titel;
            this.Gastspreker = gastspreker;
            this.Lokaal = lokaal;
            this.StartDatum = startDatum;
            this.StartUur = startUur;
            this.EindDatum = eindDatum;
            this.EindUur = eindUur;
            this.AantalOpenPlaatsen = aantalOpenPlaatsen;
            this.Beschrijving = beschrijving;
            this.Month = month;
            this.Ingeschreven = ingeschreven;
            this.Media = media;
            this.FeedbackList = feedback;
            this.Aanwezigen = aanwezigen;
        } 
        #endregion

        #region Properties
        public int SessieID { get; set; }
        public string Titel { get; set; }
        public string Gastspreker { get; set; }
        public string Lokaal { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime StartUur { get; set; }
        public DateTime EindDatum { get; set; }
        public DateTime EindUur { get; set; }
        public int AantalOpenPlaatsen { get; set; }
        public string Beschrijving { get; set; }
        public List<Media> Media { get; set; }
        public Month Month { get; set; }
        public List<Feedback> FeedbackList { get; set; }
        #endregion

        public List<UserSessie> Aanwezigen { get; set; }
        public List<UserSessie> Ingeschreven { get; set; }

        public Hoofdverantwoordelijke Hoofdverantwoordelijke
        {
            get => default;
            set
            {
            }
        }

        public Verantwoordelijke Verantwoordelijke
        {
            get => default;
            set
            {
            }
        }

        //public HashSet<User> Users { get; set; }

        #region Methods
        public bool MeldAanwezig(int sessieID ,int userID)
        {

            //user = (User) UserSessies.Select(s => s.User).Where(s => s.UserID.Equals(userID));

            //// Als gebruiker is ingeschreven en niet in de lijst van aanwezigen zit, steek in lijst aanwezigen en return true;
            //if (Ingeschrevenen.Contains(user) && !(Aanwezigen.Contains(user)))
            //{
            //    Aanwezigen.Add(user);
            //    return true;
            //}
            //else 
            return false;
            //throw new System.NotImplementedException();
        }

        public bool SchrijfIn(int sessieID, int userID)
        {
            // als gebruiker nog niet is ingeschreven dan gebruiker ingeschrijven
           // if(Ingeschrevenen.Contains() )
            throw new System.NotImplementedException();
        }

        public void OpenZetten()
        {
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