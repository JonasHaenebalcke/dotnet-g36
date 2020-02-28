﻿using System;
using System.Collections.Generic;
using System.Text;
using dotnet_g36.Models.Domain;
using Xunit;

namespace dotnet_g36.Tests.Models.Domain
{
    public class SessieTest
    {
        //NU: enkel testen met deelnemer, immers User is abstract
        // verantwoordelijken ook testen?
        private User _gebruiker;
        private Sessie _sessie;

        public SessieTest()
        {
            SessieTestInitilise();
        }

        // beter in constructor van test maken
        public void SessieTestInitilise()
        {
            _gebruiker = new Deelnemer();
            _sessie = new Sessie();
            _gebruiker.StatusGebruiker = StatusGebruiker.Actief;
            _sessie.StartDatum = DateTime.Now.AddMonths(1);
            _sessie.AantalOpenPlaatsen = 10;

        }

        //[Fact]
        //public void InschrijvenSessieTest()
        //{
        //    SessieTestInitilise();
        //    _sessie.SchrijfIn(_sessie.SessieID, _gebruiker.UserID);

        //    Assert.Contains(_gebruiker, _sessie.Ingeschrevenen);
        //}

        [Fact]
        public void InschrijvenSessieOngeligeMaandTest()
        {
            //SessieTestInitilise();
            _sessie.StartDatum = DateTime.Now.AddMonths(-2);
            Assert.Throws<ArgumentException>(
                () => _sessie.SchrijfIn(_sessie.SessieID, _gebruiker.UserID));
        }

        [Fact]
        public void InschrijvenSessieGeenPlaatsTest()
        {
            //SessieTestInitilise();
            _sessie.AantalOpenPlaatsen = 0;
            Assert.Throws<ArgumentException>(
                () => _sessie.SchrijfIn(_sessie.SessieID, _gebruiker.UserID));
        }

        [Fact]
        public void InschrijvenSessieGebruikerReedsIngeschrevenTest()
        {
            //SessieTestInitilise();
            _sessie.SchrijfIn(_sessie.SessieID, _gebruiker.UserID);

            Assert.Throws<ArgumentException>(
                () => _sessie.SchrijfIn(_sessie.SessieID, _gebruiker.UserID));
        }

        [Fact]
        public void InschrijvenSessieGebruikerNietActiefTest()
        {
            //SessieTestInitilise();
            _gebruiker.StatusGebruiker = StatusGebruiker.NietActief;
            Assert.Throws<ArgumentException>(
                () => _sessie.SchrijfIn(_sessie.SessieID, _gebruiker.UserID));
        }

        //[Fact]
        //public void UitschrijvenSessieTest()
        //{
        //    SessieTestInitilise();
        //    _sessie.SchrijfIn(_sessie.SessieID, _gebruiker.UserID);

        //    _sessie.SchrijfUit(_sessie.SessieID, _gebruiker.UserID);

        //    Assert.DoesNotContain(_gebruiker, _sessie.Ingeschrevenen);
        //}

        [Fact]
        public void UitschrijvenSessieNietIngeschrevenTest()
        {
            //SessieTestInitilise();
            Assert.Throws<ArgumentException>(
                () => _sessie.SchrijfUit(_sessie.SessieID, _gebruiker.UserID));

        }

        [Fact]
        public void SessieUitschrijvenReedsGestartTest()
        {
            //SessieTestInitilise();
            _sessie.StartDatum = DateTime.Now.AddMonths(-2);
            Assert.Throws<ArgumentException>(
                () => _sessie.SchrijfUit(_sessie.SessieID, _gebruiker.UserID));
        }

        [Fact] //fout, use usersessie
        public void FeedbackGevenTest()
        {
            //SessieTestInitilise();
            _sessie.StartDatum = DateTime.Now.AddHours(-2);
            _sessie.MeldAanwezig(_sessie.SessieID, _gebruiker.UserID);
            _sessie.FeedbackGeven();
        }

        [Fact]
        public void FeedbackGevenGebruikerNietAanwezigTest()
        {
            //SessieTestInitilise();
            _sessie.StartDatum = DateTime.Now.AddHours(-2);
            Assert.Throws<ArgumentException>(
                () => _sessie.FeedbackGeven());
        }

        [Fact]
        public void FeedbackGevenSessieNogNietGestartTest()
        {
            //SessieTestInitilise();
            _sessie.StartDatum = DateTime.Now.AddHours(2);
            _sessie.MeldAanwezig(_sessie.SessieID, _gebruiker.UserID);
            Assert.Throws<ArgumentException>(
                () => _sessie.FeedbackGeven());
        }
        /* gebruiker.Status: bool?
        * sessie.SchrijfIn: SessieID? -> gebruiker?
        * 
        */
    }
}
