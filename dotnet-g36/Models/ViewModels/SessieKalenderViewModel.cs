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

        //public SelectList Maanden { get; set; }
        //public ICollection<string> GastSprekers { get; set; }
        //public ICollection<string> Titels { get; set; }
        //public ICollection<DateTime> StartDatums { get; set; }
        //public ICollection<bool> Aanwezigheden { get; set; }
        //public ICollection<int> OpenPlaatsen { get; set; }
        //public ICollection<int> Capaciteiten { get; set; }
        //public ICollection<bool> Ingeschrevenen { get; set; }
        //public ICollection<int> SessieIds { get; set; }
        //public ICollection<bool> GeslotenList { get; set; }
        //public ICollection<string> StartDatumsFormatted { get; set; }

        public SessieKalenderViewModel() { }
        public SessieKalenderViewModel(Sessie sessie, Gebruiker gebruiker) //ALLES IN EEN
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

        //public SessieKalenderViewModel(IEnumerable<Sessie> sessies, Gebruiker gebruiker, int maandNr = 0) //LUS
        //{
        //    Maanden = GetMaandSelectList(maandNr);

        //    GastSprekers = new List<string>();
        //    Titels = new List<string>();
        //    StartDatums = new List<DateTime>();
        //    Aanwezigheden = new List<bool>();
        //    OpenPlaatsen = new List<int>();
        //    Capaciteiten = new List<int>();
        //    Ingeschrevenen = new List<bool>();
        //    SessieIds = new List<int>();
        //    GeslotenList = new List<bool>();
        //    StartDatumsFormatted = new List<string>();


        //    foreach (Sessie sessie in sessies)
        //    {
        //        GastSprekers.Add(sessie.Gastspreker);
        //        Titels.Add(sessie.Titel);
        //        StartDatums.Add(sessie.StartDatum);
        //        OpenPlaatsen.Add(sessie.GebruikerSessies.Count - 1);
        //        Capaciteiten.Add(sessie.Capaciteit);
        //        SessieIds.Add(sessie.SessieID);
        //        GeslotenList.Add(sessie.StatusSessie == StatusSessie.Gesloten);

        //        StartDatumsFormatted.Add(sessie.StartDatum.ToShortDateString() + " " + sessie.StartDatum.Hour + ":" + sessie.StartDatum.Minute);


        //        bool aanwezig = false, ingeschreven = false;
        //        if (gebruiker != null)
        //        {
        //            foreach (GebruikerSessie gebruikerSessie in sessie.GebruikerSessies)
        //            {
        //                if (gebruikerSessie.Gebruiker == gebruiker)
        //                {
        //                    aanwezig = gebruikerSessie.Aanwezig;
        //                    ingeschreven = true;
        //                    break;
        //                }
        //            }
        //        }

        //        Aanwezigheden.Add(aanwezig);
        //        Ingeschrevenen.Add(ingeschreven);
        //    }
        //}

        ///// <summary>
        ///// Retourneert selectlist van alle sessies in de opgegeven maand
        ///// </summary>
        ///// <param name="maandId">nummer van de opgegeven maand</param>
        ///// <returns>Selectlist van sessies</returns>
        //private SelectList GetMaandSelectList(int maandId/* = 0*/)
        //{
        //    var maanden = DateTimeFormatInfo.CurrentInfo.MonthNames.Select((monthName, index) => new SelectListItem { Value = (index + 1).ToString(), Text = monthName });
        //    SelectList result = new SelectList(maanden.SkipLast(1), "Value", "Text", maandId);
        //    return result;
        //}
    }
}
