using dotnet_g36.Models.Domain;
using System;
using System.Collections.Generic;

namespace dotnet_g36.Models.ViewModels
{
    public class SessieDetailsViewModel
    {
        public int sessieID { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public String Lokaal { get; set; }
        public int AantalAanwezigen { get; set; }
        public bool DeelnemerAanwezig { get; set; }
        public int OpenPlaatsen { get; set; }
        public int Capaciteit { get; set; }
        public bool DeelnemerIngeschreven { get; set; }
        public IEnumerable<Media> ListMedia { get; set; }
        public IEnumerable<Feedback> FeedbackList { get; set; }
        public string GastSpreker { get; set; }
        public Verantwoordelijke Verantwoordelijke { get; set; }
        public Verantwoordelijke Hoofdverantwoordelijke { get; set; }
        public bool Gesloten { get; set; }
        public string FeedbackContent { get; set; }

        public Gebruiker gebruiker;
        public SessieDetailsViewModel(){ }

        public SessieDetailsViewModel(Sessie sessie, Gebruiker gebruiker)
        {
            this.sessieID = sessie.SessieID;
            this.Titel = sessie.Titel;
            this.Beschrijving = sessie.Beschrijving;
            this.StartDatum = sessie.StartDatum;
            this.EindDatum = sessie.EindDatum;
            this.Lokaal = sessie.Lokaal;
            this.Capaciteit = sessie.Capaciteit;
            this.OpenPlaatsen = sessie.GebruikerSessies.Count - 1;
            this.ListMedia = sessie.Media;
            this.FeedbackList = sessie.FeedbackList;
            this.GastSpreker = sessie.Gastspreker;
            this.Verantwoordelijke = sessie.Verantwoordelijke;
            this.Gesloten = sessie.StatusSessie == StatusSessie.Gesloten;
           // this.FeedbackContent = "";
           

            this.AantalAanwezigen = 0;
            this.gebruiker = gebruiker;
            DeelnemerAanwezig = false;
            DeelnemerIngeschreven = false;

            
            foreach (GebruikerSessie gebruikersessie in sessie.GebruikerSessies)
             {
                 if (gebruiker != null && gebruikersessie.UserID == gebruiker.Id)
                 {
                     DeelnemerIngeschreven = true;
                     DeelnemerAanwezig = gebruikersessie.Aanwezig;
                 }
                 if (gebruikersessie.Aanwezig)
                     AantalAanwezigen++;
             }
        }
    }
}
