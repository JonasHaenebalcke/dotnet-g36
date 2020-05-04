using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Domain
{
    public class SessieKalender
    {
        public int SessieKalenderID { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public List<Sessie> Sessies { get; set; }

        public SessieKalender(DateTime startDatum, DateTime eindDatum)
        {
            Sessies = new List<Sessie>();
            StartDatum = startDatum;
            EindDatum = eindDatum;
        }

        public void AddSessie(Sessie sessie)
        {
            Sessies.Add(sessie);
            sessie.SessieKalender = this;
        }
    }
}
