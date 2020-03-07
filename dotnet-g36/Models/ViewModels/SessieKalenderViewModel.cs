using dotnet_g36.Data.Repositories;
using dotnet_g36.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.ViewModels
{
    public class SessieKalenderViewModel
    {
        public IEnumerable<Sessie> Sessies { get; set; }
        public SelectList Maanden { get; set; }
        public ICollection<List<string>> DetailsSessies { get; set; }
        public ICollection<string> GastSprekers { get; set; }
        public ICollection<string> Titels { get; set; }
        public ICollection<DateTime> DateTimes { get; set; }
        public ICollection<bool> Aanwezigheden { get; set; }
        public ICollection<int> OpenPlaatsen { get; set; }
        public ICollection<bool> Ingeschrevenen { get; set; }
        public ICollection<int> SessieIds { get; set; }

        public SessieKalenderViewModel() { }

        public SessieKalenderViewModel(IEnumerable<Sessie> sessies, SelectList maanden, User user)

        {

            Sessies = sessies;
            Maanden = maanden;
            //DetailsSessies = new List<List<string>>();

            GastSprekers = new List<string>();
            Titels = new List<string>();
            DateTimes = new List<DateTime>();
            Aanwezigheden = new List<bool>();
            OpenPlaatsen = new List<int>();
            Ingeschrevenen = new List<bool>();
            SessieIds = new List<int>();

            foreach (Sessie sessie in sessies)
            {
                GastSprekers.Add(sessie.Gastspreker);
                Titels.Add(sessie.Titel);
                DateTimes.Add(sessie.StartDatum);
                OpenPlaatsen.Add(sessie.AantalOpenPlaatsen);
                SessieIds.Add(sessie.SessieID);

                //String aanwezig = false.ToString(), ingeschreven = false.ToString();
                bool aanwezig = false, ingeschreven = false;
                foreach (UserSessie userSessie in sessie.UserSessies)
                {
                    if (userSessie.User.Equals(user))
                    {
                        //aanwezig = userSessie.Aanwezig.ToString();
                        //ingeschreven = true.ToString();
                        aanwezig = userSessie.Aanwezig;
                        ingeschreven = true;
                        break;
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
