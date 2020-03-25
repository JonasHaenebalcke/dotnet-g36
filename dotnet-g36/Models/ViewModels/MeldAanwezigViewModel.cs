using System;
using System.Collections.Generic;
using dotnet_g36.Models.Domain;

namespace dotnet_g36.Models.ViewModels
{
    public class MeldAanwezigViewModel
    {
        public string Barcode { get; set; }
        public IDictionary<string, string> IngeschrevenenB { get; set; }
        public int SessieID { get; set; }
        public string Titel { get; set; }
        public ICollection<string> Aanwezigen { get; set; }
        //public ICollection<string> Ingeschrevenen { get; set; }
        //public ICollection<string> Barcodes { get; set; }
        public DateTime Start { get; set; }

        public MeldAanwezigViewModel() { }

        public MeldAanwezigViewModel(Sessie sessie)
        {
            Start = sessie.StartDatum;
            SessieID = sessie.SessieID;
            Titel = sessie.Titel;
            Aanwezigen = new List<string>();
            IngeschrevenenB = new Dictionary<string, string>();
            //Ingeschrevenen = new List<string>();
            //Barcodes = new List<string>();

            foreach (Gebruiker gebruiker in sessie.geefAlleAanwezigen())
            {
                this.Aanwezigen.Add(gebruiker.GeefVolledigeNaam());
            }

            foreach (GebruikerSessie gebruikerSessie in sessie.GebruikerSessies)
            {
                //Ingeschrevenen.Add(gebruikerSessie.Gebruiker.GeefVolledigeNaam());
                //Barcodes.Add(gebruikerSessie.Gebruiker.Barcode);
                IngeschrevenenB.Add(gebruikerSessie.Gebruiker.GeefVolledigeNaam(), gebruikerSessie.Gebruiker.Barcode);
            }
        }
    }
}
