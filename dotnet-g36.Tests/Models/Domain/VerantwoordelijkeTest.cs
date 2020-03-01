using dotnet_g36.Models.Domain;
using dotnet_g36.Tests.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace dotnet_g36.Tests.Models.Domain
{
    public class VerantwoordelijkeTest
    {
        private readonly Verantwoordelijke _verantwoordelijke;
        private Verantwoordelijke _organizer1, _organizer2;
        private readonly DummyDbContext _context;
        public List<Sessie> _georganiseerdeSessies; // sessies worden toegevoegd
        public List<Sessie> _lijstSessies; // sessies worden bekeken

        public VerantwoordelijkeTest()
        {
            _context = new DummyDbContext();
            _georganiseerdeSessies = new List<Sessie>();
            _organizer1 = _context.organizer1;
            _organizer2 = _context.organizer2;
            _lijstSessies = (List<Sessie>)_context.HuidigeMaand;
        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_geenSessieOmOpenTeZetten_melding()
        {
            Assert.Equal(_georganiseerdeSessies, _verantwoordelijke.GeorganiseerdeSessies);

        }
        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_1sessieOpenzetten_gelukt()
        {
            Sessie sessie = _lijstSessies.Find(s => s.SessieID.Equals(1));
            _georganiseerdeSessies.Add(sessie);
            Assert.Equal(_organizer1, sessie.Verantwoordelijke);
            Assert.True(_verantwoordelijke.SessieOpenZetten(sessie.SessieID));
        }
        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_DieAlOpenStaat_melding()
        {
            Sessie openSessie = _lijstSessies.Find(s => s.SessieID.Equals(6));
            _georganiseerdeSessies.Add(openSessie);
            Assert.Equal(_organizer1, openSessie.Verantwoordelijke);
            Assert.False(_verantwoordelijke.SessieOpenZetten(openSessie.SessieID));
            Assert.Throws<ArgumentException>(() => { return "Je kan geen sessie openzetten die al open staat."; });
        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_dieGeslotenIs_melding()
        {
            Sessie geslotenSessie = _lijstSessies.Find(s => s.SessieID.Equals(3));
            _georganiseerdeSessies.Add(geslotenSessie);// sessie toevoegen aan lijst.
            Assert.Equal(_organizer1, geslotenSessie.Verantwoordelijke);
            Assert.False(_verantwoordelijke.SessieOpenZetten(geslotenSessie.SessieID));
            Assert.Throws<ArgumentException>(() => { return "Gesloten sessies kan je niet terug openzetten."; });
        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_dieInHetVerledenLigt_melding()
        {
            Sessie sessieVerleden = _lijstSessies.Find(s => s.SessieID.Equals(4));
            Assert.Equal(_organizer2, sessieVerleden.Verantwoordelijke);
            _georganiseerdeSessies.Add(sessieVerleden);
            Assert.NotEqual(DateTime.Now.Year, sessieVerleden.StartDatum.Year);
            Assert.False(_verantwoordelijke.SessieOpenZetten(sessieVerleden.SessieID));
            Assert.Throws<ArgumentException>(() => { return "Sessie in het verleden kan je niet terug openzetten."; });
        }

        [Fact]
        public void SessieOpenzetten_VerantwoordelijkeNietAngemaakt_melding()
        {
            Sessie sessie = _lijstSessies.Find(s => s.SessieID.Equals(4));
            _georganiseerdeSessies.Add(sessie);
            Assert.NotEqual(_organizer1, sessie.Verantwoordelijke);
            Assert.False(_verantwoordelijke.SessieOpenZetten(sessie.SessieID));
            Assert.Throws<ArgumentException>(() => { return "Sessie dat je zelf niet hebt aangemaakt kan je niet openzetten."; });
        }
        //[Fact]
        [Theory]
        [MemberData(nameof(TestCase.DatumIndex), MemberType = typeof(TestCase))]
        // public void SessieOpenzetten_VerantwoordelijkeAngemaakt_net1uurVoorStart_gelukt() 
        public void SessieOpenzetten_VerantwoordelijkeAngemaakt_MinutenVoorStart_gelukt(int i)
        {
            var a = TestCase.DataTest[i];
            DateTime dt = (DateTime)a[0];
            int min = (int)a[1];
            Sessie sessie = _lijstSessies.Find(s => s.SessieID.Equals(1));
             Assert.Equal(dt, (sessie.StartDatum.AddMinutes(-min)));
            Assert.Equal(_organizer1, sessie.Verantwoordelijke);
            _georganiseerdeSessies.Add(sessie); // sessie toevoegen aan lijst
            Assert.True(_verantwoordelijke.SessieOpenZetten(sessie.SessieID));
            Assert.Equal(StatusSessie.Open, sessie.StatusSessie);

          /*  Sessie sessie = _lijstSessies.Find(s => s.SessieID.Equals(1));
            DateTime openZettenUur = (new DateTime(2020, 3, 14, 6, 30, 00));
            Assert.Equal(openZettenUur, (sessie.StartDatum.AddMinutes(-60)));
            Assert.Equal(_organizer1, sessie.Verantwoordelijke);
            _georganiseerdeSessies.Add(sessie); // sessie toevoegen aan lijst
            Assert.True(_verantwoordelijke.SessieOpenZetten(sessie.SessieID));
            Assert.Equal(StatusSessie.Open, sessie.StatusSessie);
*/
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
            Sessie sessie = _lijstSessies.Find(s => s.SessieID.Equals(2));
            DateTime openZettenUur = (new DateTime(2020, 3, 27, 11, 20, 00));
            Assert.Equal(_organizer1, sessie.Verantwoordelijke);
            _georganiseerdeSessies.Add(sessie); // toevoegen sessie
            Assert.NotEqual(openZettenUur, (sessie.StartDatum.AddMinutes(-70)));
            Assert.False(_verantwoordelijke.SessieOpenZetten(sessie.SessieID));
            Assert.Throws<ArgumentException>(() => { return "Sessie mag pas open gezet worden 1 uur voor start van sessie"; });

        }

    }
}
