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
        public IEnumerable<Sessie> Sessies { get; set; }
        public SelectList Maanden { get; set; }
        public ICollection<string> GastSprekers { get; set; }
        public ICollection<string> Titels { get; set; }
        public ICollection<DateTime> StartDatums { get; set; }
        public ICollection<bool> Aanwezigheden { get; set; }
        public ICollection<int> OpenPlaatsen { get; set; }
        public ICollection<int> Capaciteit { get; set; }
        public ICollection<bool> Ingeschrevenen { get; set; }
        public ICollection<int> SessieIds { get; set; }
        public ICollection<bool> Gesloten { get; set; }

        //public SessieKalenderViewModel() { }

        public SessieKalenderViewModel(IEnumerable<Sessie> sessies, /* SelectList maanden,*/ Gebruiker gebruiker, int maandNr = 0)
        {
            Sessies = sessies; // alle details v alle sessies in verschillende lists (overlopen adhv for lus)
            Maanden = GetMaandSelectList(maandNr);

            GastSprekers = new List<string>();
            Titels = new List<string>();
            StartDatums = new List<DateTime>();
            Aanwezigheden = new List<bool>();
            OpenPlaatsen = new List<int>();
            Capaciteit = new List<int>();
            Ingeschrevenen = new List<bool>();
            SessieIds = new List<int>();
            Gesloten = new List<bool>();
            

            foreach (Sessie sessie in sessies)
            {
                GastSprekers.Add(sessie.Gastspreker);
                Titels.Add(sessie.Titel);
                StartDatums.Add(sessie.StartDatum);
                OpenPlaatsen.Add(sessie.GebruikerSessies.Count - 1);
                Capaciteit.Add(sessie.Capaciteit);
                SessieIds.Add(sessie.SessieID);
                Gesloten.Add(sessie.StatusSessie == StatusSessie.Gesloten);
              

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

                Aanwezigheden.Add(aanwezig);
                Ingeschrevenen.Add(ingeschreven);
            }
        }

        /// <summary>
        /// Retourneert selectlist van alle sessies in de opgegeven maand
        /// </summary>
        /// <param name="maandId">nummer van de opgegeven maand</param>
        /// <returns>Selectlist van sessies</returns>
        private SelectList GetMaandSelectList(int maandId/* = 0*/)
        {
            var maanden = DateTimeFormatInfo.CurrentInfo.MonthNames.Select((monthName, index) => new SelectListItem { Value = (index + 1).ToString(), Text = monthName });
            SelectList result = new SelectList(maanden.SkipLast(1), "Value", "Text", maandId);
            return result;
        }
    }
}
