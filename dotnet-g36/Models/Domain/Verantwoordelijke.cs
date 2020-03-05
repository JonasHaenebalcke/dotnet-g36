using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;

namespace dotnet_g36
{
    public class Verantwoordelijke : User
    {
        #region properties
        public ICollection<Sessie> OpenTeZettenSessies { get; set; }
        #endregion

        #region constructors
        public Verantwoordelijke() : base() { }
        public Verantwoordelijke(string voornaam, string familienaam, StatusGebruiker statusGebruiker, List<Sessie> sessies)
            : base(voornaam, familienaam, statusGebruiker)
        {
            OpenTeZettenSessies = sessies;
        }
        #endregion

        #region methods
        public void SessieOpenZetten(Sessie sessie)
        {
            if (OpenTeZettenSessies.Contains(sessie) && sessie.StatusSessie.Equals(StatusSessie.NietOpen) && DateTime.Now >= sessie.StartDatum.AddHours(-1))
            {
                sessie.StatusSessie = StatusSessie.Open;
            }
            else
            {
                throw new GeenSessiesException("Sessie kan niet worden opengezet.");
            }
        }

        public void SessieSluiten(Sessie sessie)
        {
            if (OpenTeZettenSessies.Contains(sessie) && sessie.StatusSessie.Equals(StatusSessie.Open))
            {
                sessie.StatusSessie = StatusSessie.Gesloten;
            }
            else
            {
                throw new GeenSessiesException("Sessie kan niet gesloten worden.");
            }
        }
        #endregion
    }
}