using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36.Models.Domain
{
    public class Media
    {
        #region Properties

        public int MediaID { get; set; }
        public string Beschrijving { get; set; }
        public string Link { get; set; }
        public string Titel { get; set; }

        #endregion

        #region Constructor
        public Media()
        {

        }

        public Media(string titel, string link, string beschrijving)
        {
            Beschrijving = beschrijving;
            Link = link;
            Titel = titel;
        }

        #endregion
    }
}