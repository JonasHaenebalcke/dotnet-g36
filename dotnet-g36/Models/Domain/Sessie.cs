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
        public IEnumerable<UserSessie> UserSessies { get; set; }
        public Hoofdverantwoordelijke Hoofdverantwoordelijke { get; set; }
        public Verantwoordelijke Verantwoordelijke { get; set; }
        public StatusSessie StatusSessie { get; set; }

        #endregion

        #region Constructors
        public Sessie() { }

        public Sessie(Hoofdverantwoordelijke hoofdVerantwoordelijke, Verantwoordelijke verantwoordelijke,
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

      
    }
}