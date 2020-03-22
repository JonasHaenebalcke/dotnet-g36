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
        private readonly Verantwoordelijke _admin, _organizer1, _organizer2, _nietActieveVerantwoordelijke, _geblokkeerdeVerantwoordelijke;
        private readonly DummyDbContext _context;
        private Sessie hedenSessie, verledenSessie, toekomstSessie;

        /* Sessie huidigeMaandSessie = new Sessie(admin, organizer1, "Sessie 3D Printing", "B1.027",
               DateTime.Now, DateTime.Now.AddHours(2),
               25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
               );*/

        public VerantwoordelijkeTest()
        {
            //_admin = new Verantwoordelijke()
            //{
            //    IsHoofdverantwoordelijke = true,
            //};

            _context = new DummyDbContext();

            _admin = _context.admin;

            _organizer1 = _context.organizer1;
            _organizer2 = _context.organizer2;
            _nietActieveVerantwoordelijke = _context.nietActieveVerantwoordelijke;
            _geblokkeerdeVerantwoordelijke = _context.geblokkeerdeVerantwoordelijke;

            hedenSessie = _context.hedenSessie;
            verledenSessie = _context.verledenSessie;
            toekomstSessie = _context.toekomstSessie;
        }

        #region VerantwoordelijkeTesten
        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_geenSessieOmOpenTeZetten_melding()
        {
            Verantwoordelijke verantwoordelijke = new Verantwoordelijke();
            Assert.Equal(new List<Sessie>(), verantwoordelijke.OpenTeZettenSessies);
        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAangemaakt_1sessieOpenzetten_gelukt()
        {
            hedenSessie.StartDatum.AddMinutes(30);
            Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
            _organizer1.OpenTeZettenSessies.Add(hedenSessie);
            hedenSessie.SessieOpenZetten(_organizer1);
            Assert.Equal(StatusSessie.Open, hedenSessie.StatusSessie);
        }

        //[Fact]
        //public void SessieOpenzetten_VerantwoordelijkeAngemaakt_DieAlOpenStaat_melding()
        //{
        //    hedenSessie = _context.hedenSessie;
        //    hedenSessie.StatusSessie = StatusSessie.Open;
        //    Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
        //    _organizer1.OpenTeZettenSessies.Add(hedenSessie);
        //    Assert.Throws<SessieException>(() => { hedenSessie.SessieOpenZetten(_organizer1); });

        //}

        //[Fact]s
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
            Assert.Throws<NullReferenceException>(() => { hedenSessie.SessieOpenZetten(_organizer2); });

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
            DateTime datum = DateTime.Now.AddMinutes(i);
            //hedenSessie = new Sessie(_organizer1, "Sessie 3D Printing", "B1.027",
            //datum, datum.AddHours(2),
            //25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
            //);
            hedenSessie.StartDatum = datum;
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
