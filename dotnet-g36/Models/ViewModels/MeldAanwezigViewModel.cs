using System;
using System.Collections.Generic;
using dotnet_g36.Models.Domain;

namespace dotnet_g36.Models.ViewModels
{
    public class MeldAanwezigViewModel
    {
        private readonly IUserRepository _userRepository;
        public String Barcode { get; set; }
        public int SessieID { get; set; }
        public String Titel { get; set; }
        public List<string> Aanwezigen { get; set; }
        public DateTime Start { get; set; }

        //public Sessie sessie;
        public MeldAanwezigViewModel() { }
        public MeldAanwezigViewModel(Sessie sessie, IUserRepository userRepository)
        {
            Start = sessie.StartDatum;
            //this.sessie = sessie;
            this.SessieID = sessie.SessieID;
            this.Titel = sessie.Titel;
            this.Aanwezigen = new List<string>();
            _userRepository = userRepository;
            foreach (Guid gebruiker in sessie.geefAlleAanwezigen())
            {
                this.Aanwezigen.Add(userRepository.GetDeelnemerByID(gebruiker).UserName);
            }


        }


    }
}
