using System;
using System.Collections.Generic;
using dotnet_g36.Models.Domain;

namespace dotnet_g36.Models.ViewModels
{
    public class MeldAanwezigViewModel
    {
        public String Barcode { get; set; }
        public int SessieID { get; set; }
        public ICollection<string> Aanwezigen { get; set; }
        public string Titel { get; set; }
        public DateTime Start { get; set; }

        //public Sessie sessie;
        public MeldAanwezigViewModel(){}
        public MeldAanwezigViewModel(Sessie sessie)
        {
            Start = sessie.StartDatum;
            //this.sessie = sessie;
            this.SessieID = sessie.SessieID;
            this.Titel = sessie.Titel;
            this.Aanwezigen = sessie.geefAlleAanwezigen(); // as List<string>;
        }


    }
}
