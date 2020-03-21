﻿using dotnet_g36.Models.Domain;
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
        public string Lokaal { get; set; }
        public int AantalAanwezigen { get; set; }
        public bool DeelnemerAanwezig { get; set; }
        public int OpenPlaatsen { get; set; }
        public int Capaciteit { get; set; }
        public bool DeelnemerIngeschreven { get; set; }
        public IEnumerable<Media> ListMedia { get; set; }
        public IEnumerable<Feedback> FeedbackList { get; set; }
        public string GastSpreker { get; set; }
        public Verantwoordelijke Verantwoordelijke { get; set; }
        public Verantwoordelijke HoofdVerantwoordelijke { get; set; }
        public bool Gesloten { get; set; }
        public string FeedbackContent { get; set; }

        public SessieDetailsViewModel(){ }

        public SessieDetailsViewModel(Sessie sessie, Gebruiker gebruiker, Verantwoordelijke hoofdVerantwoordelijke)
        {
            sessieID = sessie.SessieID;
            Titel = sessie.Titel;
            Beschrijving = sessie.Beschrijving;
            StartDatum = sessie.StartDatum;
            EindDatum = sessie.EindDatum;
            Lokaal = sessie.Lokaal;
            Capaciteit = sessie.Capaciteit;
            OpenPlaatsen = sessie.GebruikerSessies.Count - 1;
            ListMedia = sessie.Media;
            FeedbackList = sessie.FeedbackList;
            GastSpreker = sessie.Gastspreker;
            Verantwoordelijke = sessie.Verantwoordelijke;
            HoofdVerantwoordelijke = hoofdVerantwoordelijke;
            Gesloten = sessie.StatusSessie == StatusSessie.Gesloten;
           

            AantalAanwezigen = 0;
            DeelnemerAanwezig = false;
            DeelnemerIngeschreven = false;

            
            foreach (GebruikerSessie gebruikersessie in sessie.GebruikerSessies)
             {
                 if (gebruiker != null && gebruikersessie.Gebruiker == gebruiker)
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
