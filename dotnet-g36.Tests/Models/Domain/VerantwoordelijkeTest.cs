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
        private readonly Verantwoordelijke _verantwoordelijke;
        private readonly Verantwoordelijke _organizer1, _organizer2;
        private readonly DummyDbContext _context;
        public List<Sessie> _georganiseerdeSessies; // sessies worden toegevoegd
        public List<Sessie> _lijstSessies; // sessies worden bekeken
        public Sessie hedenSessie;
        public Sessie verledenSessie;

        /* Sessie huidigeMaandSessie = new Sessie(admin, organizer1, "Sessie 3D Printing", "B1.027",
               DateTime.Now, DateTime.Now.AddHours(2),
               25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
               );*/

        public VerantwoordelijkeTest()
        {
            _verantwoordelijke = new Verantwoordelijke() {
                IsHoofdverantwoordelijke = true,
            };
            _context = new DummyDbContext();
            _georganiseerdeSessies = new List<Sessie>();
            _organizer1 = _context.organizer1;
            _organizer2 = _context.organizer2;
            _lijstSessies = (List<Sessie>)_context.HuidigeMaand;
        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_geenSessieOmOpenTeZetten_melding()
        {
            // werkt niet omdat we in klasse verantwoordelijke OpenzettenSessies = openzettenSessies van parameter in constructor verantwoordelijke
            Assert.Equal(new List<Sessie>(), _verantwoordelijke.OpenTeZettenSessies);
        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_1sessieOpenzetten_gelukt()
        {
            hedenSessie = _context.hedenSessie;
            hedenSessie.StartDatum.AddMinutes(30);
            Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
            _georganiseerdeSessies.Add(hedenSessie);
            _organizer1.OpenTeZettenSessies.Add(hedenSessie);
            hedenSessie.SessieOpenZetten(_organizer1);
            Assert.Equal(StatusSessie.Open, hedenSessie.StatusSessie);

        }
        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_DieAlOpenStaat_melding()
        {
            hedenSessie = _context.hedenSessie;
            hedenSessie.StatusSessie = StatusSessie.Open;
            Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
            _georganiseerdeSessies.Add(hedenSessie);
            _organizer1.OpenTeZettenSessies.Add(hedenSessie);
            Assert.Throws<SessieException>(() => { hedenSessie.SessieOpenZetten(_organizer1); });

        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_dieGeslotenIs_melding()
        {
            hedenSessie = _context.hedenSessie;
            hedenSessie.StatusSessie = StatusSessie.Gesloten;
            Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
            _organizer1.OpenTeZettenSessies.Add(hedenSessie);
            Assert.Throws<SessieException>(() => { hedenSessie.SessieOpenZetten(_organizer1); });

        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_dieInHetVerledenLigt_melding()
        {
            verledenSessie = _context.verledenSessie;
            Assert.Equal(_organizer2, verledenSessie.Verantwoordelijke);
            _organizer2.OpenTeZettenSessies.Add(verledenSessie);
            Assert.NotEqual(DateTime.Now.Year, verledenSessie.StartDatum.Year);
            Assert.Throws<NullReferenceException>(() => { hedenSessie.SessieOpenZetten(_organizer2); });

        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeNietAngemaakt_melding()
        {
            hedenSessie = _context.hedenSessie;
            Assert.NotEqual(_organizer2, hedenSessie.Verantwoordelijke);
            Assert.Throws<SessieException>(() => { hedenSessie.SessieOpenZetten(_organizer2); });

      
        }
        [Theory]
        [InlineData(50)]
        [InlineData(60)]
        [InlineData(5)]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_MinutenVoorStart_gelukt( int i)
        {
            DateTime parsedDatum = DateTime.Now.AddMinutes(i);
            hedenSessie = new Sessie(_organizer1, "Sessie 3D Printing", "B1.027",
            parsedDatum, parsedDatum.AddHours(2),
            25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
            );
            Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
            _organizer1.OpenTeZettenSessies.Add(hedenSessie);
            hedenSessie.SessieOpenZetten(_organizer1);
            Assert.Equal(StatusSessie.Open, hedenSessie.StatusSessie);


        }
        /*  [Fact]
          public void SessieOpenzetten_5minVoorStart_gelukt() // NOG AANPASSEN
          {

              Sessie sessie = _lijstSessies.Find(s => s.SessieID.Equals(1));
              DateTime openZettenUur = (new DateTime(2020, 3, 14, 7, 25, 00));
              Assert.Equal(openZettenUur, (sessie.StartDatum.AddMinutes(-5)));
              Assert.Equal(_organizer1, sessie.Verantwoordelijke);
              _georganiseerdeSessies.Add(sessie); // sessie toevoegen aan lijst
              Assert.True(_verantwoordelijke.SessieOpenZetten(sessie.SessieID));
              Assert.Equal(StatusSessie.Open, sessie.StatusSessie);

          }
          [Fact]
          public void SessieOpenzetten_50minVoorStart_gelukt() // NOG AANPASSEN
          {

              Sessie sessie = _lijstSessies.Find(s => s.SessieID.Equals(1));
              DateTime openZettenUur = (new DateTime(2020, 3, 14, 6, 40, 00));
              Assert.Equal(openZettenUur, (sessie.StartDatum.AddMinutes(-50)));
              Assert.Equal(openZettenUur, (sessie.StartDatum.AddHours(-1)));
              Assert.Equal(_organizer1, sessie.Verantwoordelijke);
              _georganiseerdeSessies.Add(sessie); // sessie toevoegen aan lijst
              Assert.True(_verantwoordelijke.SessieOpenZetten(sessie.SessieID));
              Assert.Equal(StatusSessie.Open, sessie.StatusSessie);

          }*/

        [Fact]
        public void SessieOpenzetten_1uur10minVoorStart_melding()
        {
            // Sessie sessie = _lijstSessies.Find(s => s.SessieID.Equals(2));
            DateTime openZettenUur = (new DateTime(2020, 3, 17 , 11, 20, 00));
            hedenSessie = new Sessie(_organizer1, "Sessie 3D Printing", "B1.027",
               openZettenUur.AddMinutes(70), openZettenUur.AddHours(2),
               25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
               );
            Assert.Equal(_organizer1, hedenSessie.Verantwoordelijke);
            _organizer1.OpenTeZettenSessies.Add(hedenSessie);
            Assert.Equal(openZettenUur, (hedenSessie.StartDatum.AddMinutes(-70)));
            Assert.Throws<SessieException>(() => { hedenSessie.SessieOpenZetten(_organizer1); });

            }

    }
}
