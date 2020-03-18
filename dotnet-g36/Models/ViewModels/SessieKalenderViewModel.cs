using dotnet_g36.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace dotnet_g36.Models.ViewModels
{
    public class SessieKalenderViewModel
    {
        public IEnumerable<Sessie> Sessies { get; set; }
        public SelectList Maanden { get; set; }
        //public ICollection<List<string>> DetailsSessies { get; set; }
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

        public SessieKalenderViewModel(IEnumerable<Sessie> sessies, SelectList maanden, Gebruiker gebruiker)
        {

            Sessies = sessies; // opt indien alle details v alle sessies in verschillende lists (overlopen adhv for lus)
            Maanden = maanden;
            //DetailsSessies = new List<List<string>>(); //opt indien alles in per sessie

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
                OpenPlaatsen.Add(sessie.UserSessies.Count - 1);
                Capaciteit.Add(sessie.Capaciteit);
                SessieIds.Add(sessie.SessieID);
                Gesloten.Add(sessie.StatusSessie == StatusSessie.Gesloten);

                bool aanwezig = false, ingeschreven = false;
                if (gebruiker != null)
                {
                    foreach (UserSessie userSessie in sessie.UserSessies)
                    {
                        if (userSessie.UserID == gebruiker.Id)
                        {
                            aanwezig = userSessie.Aanwezig;
                            ingeschreven = true;
                            break;
                        }
                    }
                }

                Aanwezigheden.Add(aanwezig);
                Ingeschrevenen.Add(ingeschreven);

                //Gastspreker //Titel //Startdatum @($"&") uur //Aanwezig? //Open plaatsen //Ingeschreven?
                //DetailsSessies.Add(new List<string> { sessie.Gastspreker, sessie.Titel, sessie.StartDatum.ToString("dd MMM HH"), aanwezig , sessie.AantalOpenPlaatsen.ToString(), ingeschreven });
            }
        }


    }
}
