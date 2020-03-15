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
        //NU: enkel testen met deelnemer, immers User is abstract
        // verantwoordelijken ook testen?
        private Gebruiker _gebruiker;
        private Sessie _sessie;

        public SessieTest()
        {
            _gebruiker = new Gebruiker() {
                //UserID = 1,
                UserSessies = new List<UserSessie>(),
                StatusGebruiker = StatusGebruiker.Actief
            };
            _sessie = new Sessie() {
                SessieID = 1,
                UserSessies = new List<UserSessie>(),
                StartDatum = DateTime.Now.AddMonths(1),
                AantalOpenPlaatsen = 10
            };
        }

        [Fact]
        public void InschrijvenSessieTest()
        {
            _sessie.SchrijfIn(_gebruiker);
            Assert.Equal(1, _sessie.UserSessies.Count);
            Assert.Equal(1, _gebruiker.UserSessies.Count);
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
            _sessie.AantalOpenPlaatsen = 0;
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

            Assert.Empty(_sessie.UserSessies);
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

        [Fact(Skip = " ")] //fout, use usersessie
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
