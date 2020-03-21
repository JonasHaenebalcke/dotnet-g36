using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using Xunit;

namespace dotnet_g36.Tests.Models.Domain
{
    public class SessieTest
    {
        // verantwoordelijken ook testen?
        private Gebruiker _gebruiker;
        private Sessie _sessie;

        public SessieTest()
        {
            _gebruiker = new Gebruiker() {
                //UserID = 1,
                GebruikerSessies = new List<GebruikerSessie>(),
                StatusGebruiker = StatusGebruiker.Actief
            };
            _sessie = new Sessie() {
                SessieID = 1,
                GebruikerSessies = new List<GebruikerSessie>(),
                StartDatum = DateTime.Now.AddMonths(1),
                Capaciteit = 10
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
        public void InschrijvenSessieOngeligeMaandTest()
        {
            _sessie.StartDatum = DateTime.Now.AddMonths(-2);
            Assert.Throws<ArgumentException>(
                () => _sessie.SchrijfIn(_gebruiker));
        }

        [Fact]
        public void InschrijvenSessieGeenPlaatsTest()
        {
            _sessie.Capaciteit = 0;
            Assert.Throws<ArgumentException>(
                () => _sessie.SchrijfIn(_gebruiker));
        }

        [Fact]
        public void InschrijvenSessieGebruikerReedsIngeschrevenTest()
        {
            _sessie.SchrijfIn(_gebruiker);

            Assert.Throws<IngeschrevenException>(
                () => _sessie.SchrijfIn(_gebruiker));
        }

        [Fact]
        public void InschrijvenSessieGebruikerNietActiefTest()
        {
            _gebruiker.StatusGebruiker = StatusGebruiker.NietActief;
            Assert.Throws<GeenActieveGebruikerException>(
                () => _sessie.SchrijfIn(_gebruiker));
        }

        [Fact]
        public void InschrijvenSessieGebruikerGeblokkeerdTest()
        {
            _gebruiker.StatusGebruiker = StatusGebruiker.Geblokkeerd;
            Assert.Throws<GeenActieveGebruikerException>(
                () => _sessie.SchrijfIn(_gebruiker));
        }

        [Fact]
        public void UitschrijvenSessieTest()
        {
            _sessie.SchrijfIn(_gebruiker);

            _sessie.SchrijfUit(_gebruiker);

            Assert.Empty(_sessie.GebruikerSessies);
        }

        [Fact]
        public void UitschrijvenSessieNietIngeschrevenTest()
        {
            Assert.Throws<IngeschrevenException>(
                () => _sessie.SchrijfUit(_gebruiker));

        }

        [Fact]
        public void SessieUitschrijvenReedsGestartTest()
        {
            _sessie.StartDatum = DateTime.Now.AddMonths(-2);
            Assert.Throws<ArgumentException>(
                () => _sessie.SchrijfUit(_gebruiker));
        }

        [Fact(Skip = " ")] //fout, use gebruikerSessie
        public void FeedbackGevenTest()
        {
            _sessie.StartDatum = DateTime.Now.AddHours(-2);
            _sessie.StatusSessie = StatusSessie.Gesloten;
            _sessie.MeldAanwezig(_gebruiker);
            //_sessie.FeedbackGeven(_Deelnemer);
        }

        [Fact(Skip = " ")]
        public void FeedbackGevenGebruikerNietAanwezigTest()
        {
            _sessie.StartDatum = DateTime.Now.AddHours(-2);
            //Assert.Throws<ArgumentException>(
                //() => _sessie.FeedbackGeven(_Deelnemer));
        }

        [Fact(Skip = " ")]
        public void FeedbackGevenSessieNogNietGestartTest()
        {
            _sessie.StartDatum = DateTime.Now.AddHours(2);
            _sessie.MeldAanwezig(_gebruiker);
            //Assert.Throws<ArgumentException>(
            //    () => _sessie.FeedbackGeven(_Deelnemer));
        }
        /* gebruiker.Status: bool?
        * sessie.SchrijfIn: SessieID? -> gebruiker?
        * 
        */
    }
}
