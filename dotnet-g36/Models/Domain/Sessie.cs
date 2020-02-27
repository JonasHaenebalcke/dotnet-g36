using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnet_g36
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
        public IEnumerable<UserSessie> UserSessies { get; set; }
        public Hoofdverantwoordelijke Hoofdverantwoordelijke { get; set; }
        public Verantwoordelijke Verantwoordelijke { get; set; }
        public StatusSessie StatusSessie { get; set; }
        public Month Month { get; set; }
        #endregion

        #region Constructors
        public Sessie() { }

        public Sessie(int sessieID, Hoofdverantwoordelijke hoofdVerantwoordelijke, Verantwoordelijke verantwoordelijke,
            string titel , string lokaal, DateTime startDatum, DateTime eindDatum, int aantalOpenPlaatsen,
            string beschrijving = "", string gastspreker= "")   
        {
            this.Verantwoordelijke = verantwoordelijke;
            this.Hoofdverantwoordelijke = hoofdVerantwoordelijke;
            this.SessieID = sessieID;
            this.Titel = titel;
            this.Gastspreker = gastspreker;
            this.Lokaal = lokaal;
            this.StartDatum = startDatum;
            this.EindDatum = eindDatum;
            this.AantalOpenPlaatsen = aantalOpenPlaatsen;
            this.Beschrijving = beschrijving;
            
 
        } 
        #endregion

        #region Methods
        public bool MeldAanwezig(int sessieID ,int userID)
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