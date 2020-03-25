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
        //kan ook worden herschreven als een List<SessieOpenZettenViewModel> zonder ICollections, juist zoals bij SessieKalenderViewModel
        public ICollection<string> Titels { get; set; }
        public ICollection<DateTime> StartDatums { get; set; }
        public ICollection<int> OpenPlaatsen { get; set; }
        public ICollection<int> SessieIds { get; set; }
        public ICollection<bool> Gesloten { get; set; }

        public SessieOpenzettenViewModel(IEnumerable<Sessie> sessies)
        {
            Titels = new List<string>();
            StartDatums = new List<DateTime>();
            OpenPlaatsen = new List<int>();
            SessieIds = new List<int>();
            Gesloten = new List<bool>();

            foreach (Sessie sessie in sessies)
            {
                if (sessie.StatusSessie != StatusSessie.Gesloten)
                {
                    Titels.Add(sessie.Titel);
                    StartDatums.Add(sessie.StartDatum);
                    OpenPlaatsen.Add(sessie.Capaciteit);
                    SessieIds.Add(sessie.SessieID);
                    Gesloten.Add(sessie.StatusSessie == StatusSessie.Gesloten);
                }
            }
        }
    }
}
