using dotnet_g36.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.ViewModels
{
    public class SessieDetailsViewModel
    {
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public String Lokaal { get; set; }
        public int AantalAanwezigen { get; set; }
        public bool DeelnemerAanwezig { get; set; }
        public int OpenPlaatsen { get; set; }
        public bool DeelnemerIngeschreven { get; set; }
        public IEnumerable<Media> ListMedia { get; set; }

        public SessieDetailsViewModel(Sessie sessie)
        {
            this.Titel = sessie.Titel;
            this.Beschrijving = sessie.Beschrijving;
            this.StartDatum = sessie.StartDatum;
            this.EindDatum = sessie.EindDatum;
            this.Lokaal = sessie.Lokaal;
            //this.AantalAanwezigen = sessie.UserSessies.Count()
            //Hoe kan ik dit controleren?
            this.DeelnemerAanwezig = true;
            this.OpenPlaatsen = sessie.AantalOpenPlaatsen;
            //Hoe kan ik dit controleren?
            this.DeelnemerIngeschreven = true;
            this.ListMedia = sessie.Media;
        }
    }
}
