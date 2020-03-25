using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using Xunit;

namespace dotnet_g36.Tests.Models.Domain
{
    public class SessieTest
    {
        private Gebruiker _gebruiker;
        private Sessie _sessie;

        public SessieTest()
        {
            _gebruiker = new Gebruiker() {
                GebruikerSessies = new List<GebruikerSessie>(),
                StatusGebruiker = StatusGebruiker.Actief
            };
            _sessie = new Sessie() {
                GebruikerSessies = new List<GebruikerSessie>(),
                StartDatum = DateTime.Now.AddMonths(1),
                Capaciteit = 10,
                FeedbackList = new List<Feedback>()
            };
        }

        [Fact]
        public void InschrijvenSessieTest()
        {
            _sessie.SchrijfIn(_gebruiker);
            Assert.Equal(1, _sessie.GebruikerSessies.Count);
            Assert.Equal(1, _gebruiker.GebruikerSessies.Count);
        }

        [Fact]
        public void InschrijvenSessie_VerledenMaandTest()
        {
            _sessie.StartDatum = DateTime.Now.AddMonths(-2);
            Assert.Throws<SchrijfInSchrijfUitException>(
                () => _sessie.SchrijfIn(_gebruiker));
        }

        [Fact]
        public void InschrijvenSessieGeenPlaatsTest()
        {
            _sessie.Capaciteit = 0;
            Assert.Throws<SchrijfInSchrijfUitException>(
                () => _sessie.SchrijfIn(_gebruiker));
        }

        [Fact]
        public void InschrijvenSessieGebruikerReedsIngeschrevenTest()
        {
            _sessie.SchrijfIn(_gebruiker);

            Assert.Throws<SchrijfInSchrijfUitException>(
                () => _sessie.SchrijfIn(_gebruiker));
        }

        [Theory]
        [InlineData (StatusGebruiker.NietActief)]
        [InlineData(StatusGebruiker.Geblokkeerd)]
        public void InschrijvenSessie_GeenActieveGebruikerTest_melding(StatusGebruiker status)
        {
            _gebruiker.StatusGebruiker = status;
            Assert.Throws<GeenActieveGebruikerException>(
                () => _sessie.SchrijfIn(_gebruiker));
        }

        [Fact]
        public void UitschrijvenSessieTest()
        {
            _sessie.SchrijfIn(_gebruiker);

            _sessie.SchrijfUit(_gebruiker);

            Assert.Empty(_sessie.GebruikerSessies);
            Assert.Empty(_gebruiker.GebruikerSessies);
        }

        [Fact]
        public void UitschrijvenSessie_NietIngeschrevenTest_melding()
        {
            Assert.Empty(_sessie.GebruikerSessies);
            Assert.Throws<SchrijfInSchrijfUitException>(
                () => _sessie.SchrijfUit(_gebruiker));
        }

        [Fact]
        public void SessieUitschrijven_VerledenSessie_melding()
        {
            _sessie.StartDatum = DateTime.Now.AddMonths(-2);
            Assert.Throws<SchrijfInSchrijfUitException>(
                () => _sessie.SchrijfUit(_gebruiker));
        }

        [Fact]
        public void FeedbackGevenTest()
        {
            _sessie.StartDatum = DateTime.Now.AddHours(-2);
            _sessie.StatusSessie = StatusSessie.Gesloten;
            GebruikerSessie gebruikerSessie = new GebruikerSessie(_sessie, _gebruiker) { Aanwezig = true };
            _sessie.GebruikerSessies.Add(gebruikerSessie);
            _gebruiker.GebruikerSessies.Add(gebruikerSessie);
            _sessie.FeedbackGeven("test",_gebruiker, 3);
            Assert.Equal(1, _sessie.FeedbackList.Count);
            Feedback feedback = null;
            foreach (Feedback f in _sessie.FeedbackList)
                feedback = f;
            Assert.Equal("test", feedback.Tekst);
        }

        [Theory]
        [InlineData(StatusGebruiker.NietActief)]
        [InlineData(StatusGebruiker.Geblokkeerd)]
        public void FeedbackGeven_NietActieveGebruikerTest_Fout(StatusGebruiker status)
        {
            _sessie.StartDatum = DateTime.Now.AddHours(-2);
            _sessie.StatusSessie = StatusSessie.Gesloten;
            GebruikerSessie gebruikerSessie = new GebruikerSessie(_sessie, _gebruiker) { Aanwezig = true };
            _sessie.GebruikerSessies.Add(gebruikerSessie);
            _gebruiker.GebruikerSessies.Add(gebruikerSessie);
            _gebruiker.StatusGebruiker = status;
            Assert.Throws<GeenActieveGebruikerException>(() => _sessie.FeedbackGeven("test", _gebruiker, 3));
            Assert.Empty(_sessie.FeedbackList);
        }

        [Fact]
        public void FeedbackGevenGebruikerNietIngeschrevenTest()
        {
            _sessie.StartDatum = DateTime.Now.AddHours(-2);
            _sessie.StatusSessie = StatusSessie.Gesloten;
            Assert.Throws<AanwezigException>(() => _sessie.FeedbackGeven("test", _gebruiker, 3));
            Assert.Empty(_sessie.FeedbackList);
        }

        [Fact]
        public void FeedbackGevenGebruikerNietAanwezigTest()
        {
            _sessie.StartDatum = DateTime.Now.AddHours(-2);
            _sessie.StatusSessie = StatusSessie.Gesloten;
            GebruikerSessie gebruikerSessie = new GebruikerSessie(_sessie, _gebruiker) { Aanwezig = false };
            _sessie.GebruikerSessies.Add(gebruikerSessie);
            _gebruiker.GebruikerSessies.Add(gebruikerSessie);
            Assert.Throws<AanwezigException>(() => _sessie.FeedbackGeven("test", _gebruiker, 3));
            Assert.Empty(_sessie.FeedbackList);
        }

        [Theory]
        [InlineData(StatusSessie.NietOpen)]
        [InlineData(StatusSessie.Open)]
        public void FeedbackGevenSessieNogNietGestartTest(StatusSessie status)
        {
            _sessie.StartDatum = DateTime.Now.AddHours(2);
            _sessie.StatusSessie = status;
            GebruikerSessie gebruikerSessie = new GebruikerSessie(_sessie, _gebruiker) { Aanwezig = true };
            _sessie.GebruikerSessies.Add(gebruikerSessie);
            _gebruiker.GebruikerSessies.Add(gebruikerSessie);
            Assert.Throws<FeedbackException>(() => _sessie.FeedbackGeven("test", _gebruiker, 3));
            Assert.Empty(_sessie.FeedbackList);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(6)]
        [InlineData(10)]
        public void FeedbackGevenScoreOngeldig(int value)
        {
            Assert.Throws<FeedbackException>(() => _sessie.FeedbackGeven("test", _gebruiker, value));
            Assert.Empty(_sessie.FeedbackList);
        }

        [Fact]
        public void AanwezigheidRegistrerenTest()
        {
            _sessie.StartDatum = DateTime.Now.AddMinutes(50);
            _sessie.StatusSessie = StatusSessie.Open;
            GebruikerSessie gebruikerSessie = new GebruikerSessie(_sessie, _gebruiker) { Aanwezig = false };
            _sessie.GebruikerSessies.Add(gebruikerSessie);
            _gebruiker.GebruikerSessies.Add(gebruikerSessie);

            _sessie.MeldAanwezigAfwezig(_gebruiker);
            Assert.Equal(1, _sessie.GebruikerSessies.Count);
            Assert.True(_sessie.GebruikerSessies.Single().Aanwezig);
            Assert.Single(_sessie.geefAlleAanwezigen());
        }

        [Fact]
        public void AanwezigheidRegistreren_ReedsAanwezigTest()
        {
            _sessie.StartDatum = DateTime.Now.AddMinutes(50);
            _sessie.StatusSessie = StatusSessie.Open;
            GebruikerSessie gebruikerSessie = new GebruikerSessie(_sessie, _gebruiker) { Aanwezig = true };
            _sessie.GebruikerSessies.Add(gebruikerSessie);
            _gebruiker.GebruikerSessies.Add(gebruikerSessie);

            Assert.Equal(1, _sessie.GebruikerSessies.Count);

            Assert.Throws<SchrijfInSchrijfUitException>(() => _sessie.MeldAanwezigAfwezig(_gebruiker));
            Assert.True(_sessie.GebruikerSessies.Single().Aanwezig);
            Assert.Single(_sessie.geefAlleAanwezigen());
        }

        [Theory]
        [InlineData(StatusGebruiker.Geblokkeerd)]
        [InlineData(StatusGebruiker.NietActief)]
        public void AanwezigheidRegistreren_GebruikerNietActiefTest(StatusGebruiker status)
        {
            _gebruiker.StatusGebruiker = status;
            _sessie.StartDatum = DateTime.Now.AddMinutes(50);
            _sessie.StatusSessie = StatusSessie.Open;
            GebruikerSessie gebruikerSessie = new GebruikerSessie(_sessie, _gebruiker) { Aanwezig = false };
            _sessie.GebruikerSessies.Add(gebruikerSessie);
            _gebruiker.GebruikerSessies.Add(gebruikerSessie);

            Assert.Throws<GeenActieveGebruikerException>(() => _sessie.MeldAanwezigAfwezig(_gebruiker));
            Assert.False(_sessie.GebruikerSessies.Single().Aanwezig);
            Assert.Empty(_sessie.geefAlleAanwezigen());
        }

        [Fact]
        public void AanwezigheidRegistreren_NietIngeschrevenTest()
        {
            _sessie.StartDatum = DateTime.Now.AddMinutes(50);
            _sessie.StatusSessie = StatusSessie.Open;


            Assert.Throws<SchrijfInSchrijfUitException>(() => _sessie.MeldAanwezigAfwezig(_gebruiker));
            Assert.Empty(_sessie.geefAlleAanwezigen());
        }
    }
}

