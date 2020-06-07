using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using dotnet_g36.Tests.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace dotnet_g36.Tests.Models.Domain
{
    public class VerantwoordelijkeTest
    {
        private readonly Gebruiker _admin, _organizer1, _organizer2;
        private readonly DummyDbContext _context;
        private Sessie hedenSessie, verledenSessie;

        public VerantwoordelijkeTest()
        {
            _context = new DummyDbContext();

            _admin = _context.admin;

            _organizer1 = _context.organizer1;
            _organizer2 = _context.organizer2;

            hedenSessie = _context.hedenSessie;
            verledenSessie = _context.verledenSessie;
        }

        #region VerantwoordelijkeTesten
        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_geenSessieOmOpenTeZetten_melding()
        {
            Gebruiker verantwoordelijke = new Gebruiker();
            Assert.Equal(new List<Sessie>(), verantwoordelijke.OpenTeZettenSessies);
        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAangemaakt_1sessieOpenzetten_gelukt()
        {
            hedenSessie.StartDatum = hedenSessie.StartDatum.AddMinutes(30);
            Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
            _organizer1.OpenTeZettenSessies.Add(hedenSessie);
            hedenSessie.SessieOpenZetten(_organizer1);
            Assert.Equal(StatusSessie.Open, hedenSessie.StatusSessie);
        }

        [Theory]
        [InlineData(StatusSessie.Open)]
        [InlineData(StatusSessie.Gesloten)]
        public void SessieOpenzetten_VerantwoordelijkeAangemaakt_dieGeslotenIs_melding(StatusSessie statusSessie)
        {
            hedenSessie.StatusSessie = statusSessie;
            Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
            _organizer1.OpenTeZettenSessies.Add(hedenSessie);
            Assert.Throws<SessieException>(() => { hedenSessie.SessieOpenZetten(_organizer1); });
        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_dieInHetVerledenLigt_melding()
        {
            Assert.Equal(_organizer2, verledenSessie.Verantwoordelijke);
            _organizer2.OpenTeZettenSessies.Add(verledenSessie);
            Assert.NotEqual(DateTime.Now.Year, verledenSessie.StartDatum.Year);
            Assert.Throws<SessieException>(() => { hedenSessie.SessieOpenZetten(_organizer2); });
        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeNietAngemaakt_melding()
        {
            Assert.NotEqual(_organizer2, hedenSessie.Verantwoordelijke);
            Assert.Throws<SessieException>(() => { hedenSessie.SessieOpenZetten(_organizer2); });
        }

        [Theory]
        [InlineData(50)]
        [InlineData(60)]
        [InlineData(5)]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_MinutenVoorStart_gelukt(int i)
        {
            hedenSessie.StartDatum = DateTime.Now.AddMinutes(i);
            Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
            _organizer1.OpenTeZettenSessies.Add(hedenSessie);
            hedenSessie.SessieOpenZetten(_organizer1);
            Assert.Equal(StatusSessie.Open, hedenSessie.StatusSessie);
        }

        [Fact]
        public void SessieOpenzetten_1uur10minVoorStart_melding()
        {
            hedenSessie.StartDatum = hedenSessie.StartDatum.AddMinutes(70);
            Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
            _organizer1.OpenTeZettenSessies.Add(hedenSessie);
            Assert.Throws<SessieException>(() => { hedenSessie.SessieOpenZetten(_organizer1); });
        }
        #endregion

        #region HoofdVerantwoordelijkeTesten
        [Fact]
        public void SessieOpenzetten_HoofdVerantwoordelijkeAangemaakt_1sessieOpenzetten_gelukt()
        {
            hedenSessie.StartDatum.AddMinutes(30);
            Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
            hedenSessie.SessieOpenZetten(_admin);
            Assert.Equal(StatusSessie.Open, hedenSessie.StatusSessie);
        }

        [Theory]
        [InlineData(StatusSessie.Open)]
        [InlineData(StatusSessie.Gesloten)]
        public void SessieOpenzetten_HoofdVerantwoordelijkeNietAangemaakt_dieGeslotenIs_melding(StatusSessie statusSessie)
        {
            hedenSessie.StatusSessie = statusSessie;
            Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
            Assert.Throws<SessieException>(() => { hedenSessie.SessieOpenZetten(_admin); });
        }
        #endregion
    }
}
