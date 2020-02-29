using dotnet_g36.Models.Domain;
using dotnet_g36.Tests.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace dotnet_g36.Tests.Models.Domain
{
    public class HoofdverantwoordelijkeTest
    {
        private readonly Hoofdverantwoordelijke _hoofdverantwoordelijke;
        private readonly DummyDbContext _context;
        private readonly Verantwoordelijke _organizer;

        private readonly List<Sessie> _alleSessiesLijst;

        // constructorTest
        public HoofdverantwoordelijkeTest()
        {
            _hoofdverantwoordelijke = new Hoofdverantwoordelijke("Sven", "Stevens", 16, StatusGebruiker.Actief);
            _context = new DummyDbContext();
            _alleSessiesLijst = new List<Sessie>();
            _organizer = new Verantwoordelijke("Organiser", "De SubAdmin", 1, StatusGebruiker.Actief);

        }

        // Er is toch een verschil tussen "Er zijn geen sessies" -> 0 sessies in lijst  en 
        // "er zijn geen sessies meer die je kan open zetten" -> er staan sessies in de lijst, maar geen één die je kan open zetten
        [Fact]
        public void SessieOpenZetten_geenSessiesOmOpenTeZetten_melding()
        {
          Assert.Equal(_alleSessiesLijst, _hoofdverantwoordelijke.AlleSessies) ;
           // Assert.Throws<ArgumentException>(() => { return "Er zijn geen sessies om open te zetten"; });
        }

        [Fact (Skip =" ")]
        public void SessieOpenZetten_1SessieOpenZetten_gelukt()
        {
            //  _alleSessiesLijst.Add(_context.SessieHoofd); // Sessie toevoegen aan lijst
            //   _context.SessieHoofd.StatusSessie = StatusSessie.Open; // Sessie openzetten
           
            Assert.True(_hoofdverantwoordelijke.SessieOpenZetten(_context.SessieHoofd.SessieID));
        }


        //klopt nog niet volledig
        [Fact(Skip = " ")]
        public void SessieOpenZetten_DieAlOpenStaat_melding()
        {
            Sessie sessie = new Sessie(8, _hoofdverantwoordelijke, _organizer, "Java", "B3.15", new DateTime(2020, 3, 14, 12, 30, 00), 
                new DateTime(2020, 3, 14, 13, 30, 00), 24, StatusSessie.Open);
            Assert.False(_hoofdverantwoordelijke.SessieOpenZetten(sessie.SessieID));
            //Assert.Throws<ArgumentException>(() => { return "Sessie is al open."; });
        }


        //klopt nog niet volledig
        [Fact (Skip = " ")] 
        public void SessieOpenZetten_dieGeslotenIs_melding()
        {
            Sessie sessie = new Sessie(8, _hoofdverantwoordelijke, _organizer , "Java", "B3.15", new DateTime(2020, 3, 14, 12, 30, 00), 
                new DateTime(2020, 3, 14, 13, 30, 00), 24, StatusSessie.Gesloten);
            Assert.False(_hoofdverantwoordelijke.SessieOpenZetten(sessie.SessieID));
          //  Assert.NotEqual(StatusSessie.Open, sessie.StatusSessie);
           Assert.Throws<ArgumentException>(() => { return "Gesloten sessie kan je niet terug open zetten."; });
        }

        //klopt nog niet volledig
        [Fact (Skip =" ")]
        public void SessieOpenZetten_dieInHetVerledenLigt_melding()
        {
            Sessie sessie = new Sessie(8, _hoofdverantwoordelijke, _organizer, "Java", "B3.15", new DateTime(2016, 3, 14, 12, 30, 00), 
                new DateTime(2016, 3, 14, 13, 30, 00), 24, StatusSessie.Gesloten);
            Assert.NotEqual(DateTime.Now.Year, sessie.StartDatum.Year);
            Assert.False(_hoofdverantwoordelijke.SessieOpenZetten(sessie.SessieID));
            //  Assert.Throws<ArgumentException>(() => { return "Gesloten sessie kan je niet trg open zetten."; });
        }
        [Fact(Skip = " ")]
        public void SessieOpenZetten_net1uurVoorStart_gelukt()
        {
            Sessie sessie = new Sessie(8, _hoofdverantwoordelijke, _organizer, "Java", "B3.15", new DateTime(2020, 3, 14, 12, 30, 00),
                new DateTime(2020, 3, 14, 13, 30, 00), 24, StatusSessie.NietOpen);
            DateTime openZettenUur = (new DateTime(2020, 3, 14, 11, 30, 00));
            Assert.Equal(openZettenUur.Hour, (sessie.StartDatum.Hour - 1));
            Assert.True(_hoofdverantwoordelijke.SessieOpenZetten(sessie.SessieID));

            //sessie.StatusSessie = StatusSessie.Open;
            //Assert.Equal(StatusSessie.Open, sessie.StatusSessie);

        }
        [Fact(Skip = " ")]
        public void SessieOpenZetten_5minVoorStart_gelukt()
        {
            Sessie sessie = new Sessie(8, _hoofdverantwoordelijke, _organizer, "Java", "B3.15", new DateTime(2020, 3, 14, 12, 30, 00), 
                new DateTime(2020, 3, 14, 13, 30, 00), 24, StatusSessie.NietOpen);
            DateTime openZettenUur = (new DateTime(2020, 3, 14, 12, 25, 00));
            Assert.Equal(openZettenUur.Hour, (sessie.StartDatum.Hour));
            Assert.Equal(openZettenUur.Minute, (sessie.StartDatum.Minute - 5));
            // sessie.StatusSessie = StatusSessie.Open;
            Assert.True(_hoofdverantwoordelijke.SessieOpenZetten(sessie.SessieID));
           // Assert.Equal(StatusSessie.Open, sessie.StatusSessie);

        }
        [Fact(Skip = " ")]
        public void SessieOpenZetten_50minVoorStart_gelukt()
        {
            Sessie sessie = new Sessie(8, _hoofdverantwoordelijke, _organizer, "Java", "B3.15", new DateTime(2020, 3, 14, 12, 30, 00),
                new DateTime(2020, 3, 14, 13, 30, 00), 24, StatusSessie.NietOpen);
            DateTime openZettenUur = (new DateTime(2020, 3, 14, 11, 40, 00));
            Assert.Equal(openZettenUur.Hour, (sessie.StartDatum.Hour-1));
            Assert.Equal(openZettenUur.Minute, (sessie.StartDatum.Minute - 10));
            sessie.StatusSessie = StatusSessie.Open;
            Assert.Equal(StatusSessie.Open, sessie.StatusSessie);

        }
        //klopt nog niet volledig
        [Fact (Skip =" ")]
        public void SessieOpenZetten_1uur10minVoorStart_melding()
        {
            Sessie sessie = new Sessie(8, _hoofdverantwoordelijke, _organizer, "Java", "B3.15", new DateTime(2020, 3, 14, 12, 30, 00), 
                new DateTime(2020, 3, 14, 13, 30, 00), 24, StatusSessie.NietOpen);
            DateTime openZettenUur = (new DateTime(2020, 3, 14, 11, 20, 00));
            Assert.Equal(openZettenUur.Hour, (sessie.StartDatum.Hour - 1));
            Assert.NotEqual(openZettenUur.Minute, (sessie.StartDatum.Minute));
            Assert.False(_hoofdverantwoordelijke.SessieOpenZetten(sessie.SessieID));
          //  Assert.Throws<ArgumentException>(() => { return "Sessie mag pas open gezet worden 1 uur voor start van sessie"; });

        }


    }
}
