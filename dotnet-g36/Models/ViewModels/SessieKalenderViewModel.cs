using dotnet_g36.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace dotnet_g36.Models.ViewModels
{
    public class SessieKalenderViewModel
    {
        public string GastSpreker { get; set; }
        public string Titel { get; set; }
        public DateTime StartDatum { get; set; }
        public bool Aanwezigheid { get; set; }
        public int OpenPlaats { get; set; }
        public int Capaciteit { get; set; }
        public bool Ingeschreven { get; set; }
        public int SessieId { get; set; }
        public bool Gesloten { get; set; }

        public SessieKalenderViewModel() { }
        public SessieKalenderViewModel(Sessie sessie, Gebruiker gebruiker)
        {
            GastSpreker = sessie.Gastspreker;
            Titel = sessie.Titel;
            StartDatum = sessie.StartDatum;
            OpenPlaats = sessie.GebruikerSessies.Count - 1;
            Capaciteit = sessie.Capaciteit;
            SessieId = sessie.SessieID;
            Gesloten = (sessie.StatusSessie == StatusSessie.Gesloten);

            bool aanwezig = false, ingeschreven = false;
            if (gebruiker != null)
            {
                foreach (GebruikerSessie gebruikerSessie in sessie.GebruikerSessies)
                {
                    if (gebruikerSessie.Gebruiker == gebruiker)
                    {
                        aanwezig = gebruikerSessie.Aanwezig;
                        ingeschreven = true;
                        break;
                    }
                }
            }
            Aanwezigheid = aanwezig;
            Ingeschreven = ingeschreven;
        }
    }
}
