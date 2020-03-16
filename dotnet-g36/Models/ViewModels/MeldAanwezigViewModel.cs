using System;
using dotnet_g36.Models.Domain;

namespace dotnet_g36.Models.ViewModels
{
    public class MeldAanwezigViewModel
    {
        public String Barcode { get; set; }
        public Sessie sessie;
        public MeldAanwezigViewModel() {}
        public MeldAanwezigViewModel(Sessie sessie)
        {
            this.sessie = sessie;
        }

        
    }
}
