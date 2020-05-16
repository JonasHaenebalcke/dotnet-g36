using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Domain
{
    public class Aankondiging
    {
        public int AankondigingID { get; set; }
        public string Titel { get; set; }
        public string Tekst { get; set; }
        public DateTime DatumAangemaakt { get; set; }
        public bool IsVerzonden { get; set; }
        public Sessie Sessie { get; set; }
        public Gebruiker Publicist { get; set; }


        public Aankondiging()
        {

        }
        public Aankondiging(string titel, string tekst, DateTime datumAangemaakt, bool isVerzonden, Sessie sessie, Gebruiker publicist)
        {
            Titel = titel;
            Tekst = tekst;
            DatumAangemaakt = datumAangemaakt;
            IsVerzonden = isVerzonden;
            Sessie = sessie;
            Publicist = publicist;
        }
    }
}
