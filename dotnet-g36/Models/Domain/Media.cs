using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36.Models.Domain
{
    public class Media
    {
        //Is nodig voor databank, kan later eventueel verwijderd worden
        public int MediaID
        {
            get => default;
            set
            {
            }
        }

        public string Beschrijving { get; set; }
        public string Link { get; set; }
    }
}