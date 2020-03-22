using System;
using System.Collections.Generic;
using dotnet_g36.Models.Domain;

namespace dotnet_g36.Models.ViewModels
{
    public class MeldAanwezigViewModel
    {
        public string Barcode { get; set; }
        public int SessieID { get; set; }
        public string Titel { get; set; }
        public ICollection<string> Aanwezigen { get; set; }
        public DateTime Start { get; set; }
        public MeldAanwezigViewModel() { }
        public MeldAanwezigViewModel(Sessie sessie)
        {
            Start = sessie.StartDatum;
            this.SessieID = sessie.SessieID;
            this.Titel = sessie.Titel;
            this.Aanwezigen = new List<string>();
            foreach (Gebruiker gebruiker in sessie.geefAlleAanwezigen())
            {
                this.Aanwezigen.Add(gebruiker.GeefVolledigeNaam());
            }
        }
    }
}
