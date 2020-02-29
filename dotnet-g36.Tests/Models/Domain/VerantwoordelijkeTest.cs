using dotnet_g36.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace dotnet_g36.Tests.Models.Domain
{
    public class VerantwoordelijkeTest
    {
        private readonly Verantwoordelijke _organizer, _organizer2;
        private readonly Hoofdverantwoordelijke _hoofdverantwoordelijke;
        private Sessie sessie3;
        public VerantwoordelijkeTest()
        {
            _hoofdverantwoordelijke = new Hoofdverantwoordelijke("Sven", "Stevens", 16, StatusGebruiker.Actief);

            _organizer = new Verantwoordelijke("Organiser", "De SubAdmin", 1, StatusGebruiker.Actief);
            _organizer2 = new Verantwoordelijke("Organiser2", "De SubAdmin2", 2, StatusGebruiker.Actief);
            sessie3 = new Sessie(3,_hoofdverantwoordelijke, _organizer, "Omgaan met frustratie problemen", "B4.012",
                new DateTime(2020, 2, 20, 12, 30, 0), new DateTime(2020, 2, 20, 13, 30, 0),
                25, StatusSessie.NietOpen, "Lucas legt in deze lezing uit hoe je moet omgaan met frustratie's uit het dagelijkse leven", "Lucas Van De Haegen"
                );
        }

        [Fact]
        public void SessieOpenZetten_VerantwoordelijkeAngemaakt_1sessieOpenZetten_gelukt()
        {
            Assert.Equal(_organizer, sessie3.Verantwoordelijke);
            sessie3.StatusSessie = StatusSessie.Open;
            Assert.Equal(StatusSessie.Open, sessie3.StatusSessie);
        }

        [Fact]
        public void SessieOpenZetten_VerantwoordelijkeAngemaakt_geenSessieOm_melding()
        {
            IEnumerable<Sessie> georganiseerdeSessie = _organizer.GeorganiseerdeSessies;
            
        }


    }
}
