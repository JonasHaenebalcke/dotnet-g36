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
        private User _User;
        private Sessie _sessie;

        public SessieTest()
        {
            _User = new User() {
                UserID = 1,
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
            _sessie.SchrijfIn(_User);
            Assert.Equal(1, _sessie.UserSessies.Count);
            Assert.Equal(1, _User.UserSessies.Count);
        }

        [Fact]
        public void InschrijvenSessieOngeligeMaandTest()
        {
            _sessie.StartDatum = DateTime.Now.AddMonths(-2);
            Assert.Throws<ArgumentException>(
                () => _sessie.SchrijfIn(_User));
        }

        [Fact]
        public void InschrijvenSessieGeenPlaatsTest()
        {
            _sessie.AantalOpenPlaatsen = 0;
            Assert.Throws<ArgumentException>(
                () => _sessie.SchrijfIn(_User));
        }

        [Fact]
        public void InschrijvenSessieGebruikerReedsIngeschrevenTest()
        {
            _sessie.SchrijfIn(_User);

            Assert.Throws<AlIngeschrevenException>(
                () => _sessie.SchrijfIn(_User));
        }

        [Fact]
        public void InschrijvenSessieGebruikerNietActiefTest()
        {
            _User.StatusGebruiker = StatusGebruiker.NietActief;
            Assert.Throws<GeenActieveGebruikerException>(
                () => _sessie.SchrijfIn(_User));
        }

        [Fact]
        public void InschrijvenSessieGebruikerGeblokkeerdTest()
        {
            _User.StatusGebruiker = StatusGebruiker.Geblokkeerd;
            Assert.Throws<GeenActieveGebruikerException>(
                () => _sessie.SchrijfIn(_User));
        }

        [Fact]
        public void UitschrijvenSessieTest()
        {
            _sessie.SchrijfIn(_User);

            _sessie.SchrijfUit(_User);

            Assert.Empty(_sessie.UserSessies);
        }

        [Fact]
        public void UitschrijvenSessieNietIngeschrevenTest()
        {
            Assert.Throws<NietIngeschrevenException>(
                () => _sessie.SchrijfUit(_User));

        }

        [Fact]
        public void SessieUitschrijvenReedsGestartTest()
        {
            _sessie.StartDatum = DateTime.Now.AddMonths(-2);
            Assert.Throws<ArgumentException>(
                () => _sessie.SchrijfUit(_User));
        }

        [Fact(Skip = " ")] //fout, use usersessie
        public void FeedbackGevenTest()
        {
            _sessie.StartDatum = DateTime.Now.AddHours(-2);
            _sessie.StatusSessie = StatusSessie.Gesloten;
            _sessie.MeldAanwezig(_User);
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
            _sessie.MeldAanwezig(_User);
            //Assert.Throws<ArgumentException>(
            //    () => _sessie.FeedbackGeven(_Deelnemer));
        }
        /* gebruiker.Status: bool?
        * sessie.SchrijfIn: SessieID? -> gebruiker?
        * 
        */
    }
}
