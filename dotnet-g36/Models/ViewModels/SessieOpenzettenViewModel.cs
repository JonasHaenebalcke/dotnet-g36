using dotnet_g36.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.ViewModels
{
    public class SessieOpenzettenViewModel
    {
        public ICollection<Verantwoordelijke> Verantwoordelijken { get; set; }
        public ICollection<string> Titels { get; set; }
        public ICollection<DateTime> StartDatums { get; set; }
        public ICollection<int> OpenPlaatsen { get; set; }
        public ICollection<int> SessieIds { get; set; }

        //public SessieOpenzettenViewModel() { }

        public SessieOpenzettenViewModel(IEnumerable<Sessie> sessies)
        {
            Verantwoordelijken = new List<Verantwoordelijke>();
            Titels = new List<string>();
            StartDatums = new List<DateTime>();
            OpenPlaatsen = new List<int>();
            SessieIds = new List<int>();

            foreach (Sessie sessie in sessies)
            {
                Verantwoordelijken.Add(sessie.Verantwoordelijke);
                Titels.Add(sessie.Titel);
                StartDatums.Add(sessie.StartDatum);
                OpenPlaatsen.Add(sessie.AantalOpenPlaatsen);
                SessieIds.Add(sessie.SessieID);              
            }
        }
    }
}
