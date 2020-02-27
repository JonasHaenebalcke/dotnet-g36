﻿using dotnet_g36.Models.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36.Tests.Data
{
    class DummyDbContext
    {
        public IEnumerable<Sessie> December;
        public IEnumerable<Sessie> Januari;
        public IEnumerable<Sessie> Februari;

        public DummyDbContext()
        {

            Hoofdverantwoordelijke admin = new Hoofdverantwoordelijke("Admin", "De Padwin", 0, StatusGebruiker.Actief);
            Verantwoordelijke organizer = new Verantwoordelijke("Organiser", "De SubAdmin", 1, StatusGebruiker.Actief);

            User Pieter = new Deelnemer("Pieter", "De Snieter", 2 , StatusGebruiker.Actief);
            User Aaron = new Deelnemer("Aaron", "Slaerm", 3, StatusGebruiker.Actief);
            User Lucifer = new Deelnemer("Lucifer", "De Duvel", 666, StatusGebruiker.Actief);

            IEnumerable<User> deelnemers = new List<User>
            {
                Pieter,
                Aaron,
                Lucifer
            };



            //Maand zit in de constuctor, maar dit is fout
            // Ik denk ook dat er extra attribuut moet komen voor ingeschreven
            Sessie sessie1 = new Sessie(1, admin, organizer, "Sessie 3D Printing", "Stefaan De Cock", "B1.027",
                new DateTime(2020, 2, 24), new DateTime(2020, 2, 24, 7, 30, 0), new DateTime(2020, 2, 24), new DateTime(2020, 2, 24, 9, 30, 0),
                25, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", Month.Februari
                //, List<User> /*INSERT USERS HERE*/, List<Media> /*INSERT MEDIA*/,
                //List<Feedback> /*INSERT FEEDBACK HERE*/, List<UserSessie> /*INSERT AANWEZIGEN HIER*/);
                );

            Sessie sessie2 = new Sessie(2, admin, organizer, "Sessie Netflix", "Jonas Haenebalcke", "BCON",
                new DateTime(2020, 2, 27), new DateTime(2020, 2, 27, 12, 30, 0), new DateTime(2020, 2, 24), new DateTime(2020, 2, 3, 13, 30, 0),
                150, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", Month.Februari
                //, List<User> /*INSERT USERS HERE*/, List<Media> /*INSERT MEDIA*/,
                //List<Feedback> /*INSERT FEEDBACK HERE*/, List<UserSessie> /*INSERT AANWEZIGEN HIER*/);
                );

            Sessie sessie3 = new Sessie(3, admin, organizer, "Omgaan met frustratie problemen", "Lucas Van De Haegen", "B4.012",
                new DateTime(2020, 2, 20), new DateTime(2020, 2, 20, 12, 30, 0), new DateTime(2020, 2, 20), new DateTime(2020, 2, 20, 13, 30, 0),
                25, "Lucas legt in deze lezing uit hoe je moet omgaan met frustratie's uit het dagelijkse leven", Month.Februari
                //, List<User> /*INSERT USERS HERE*/, List<Media> /*INSERT MEDIA*/,
                //List<Feedback> /*INSERT FEEDBACK HERE*/, List<UserSessie> /*INSERT AANWEZIGEN HIER*/);
                );

            Sessie sessie4 = new Sessie(1, admin, organizer, "Sessie 3D Printing", "Stefaan De Cock", "B1.027",
                new DateTime(2019, 12, 24), new DateTime(2019, 12, 24, 7, 30, 0), new DateTime(2019, 12, 24), new DateTime(2019, 12, 24, 9, 30, 0),
                25, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", Month.December
                //, List<User> /*INSERT USERS HERE*/, List<Media> /*INSERT MEDIA*/,
                //List<Feedback> /*INSERT FEEDBACK HERE*/, List<UserSessie> /*INSERT AANWEZIGEN HIER*/);
                );

            Sessie sessie5 = new Sessie(2, admin, organizer, "Sessie Netflix", "Jonas Haenebalcke", "BCON",
                new DateTime(2019, 12, 27), new DateTime(2019, 12, 27, 12, 30, 0), new DateTime(2019, 12, 24), new DateTime(2019, 12, 3, 13, 30, 0),
                150, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", Month.December
                //, List<User> /*INSERT USERS HERE*/, List<Media> /*INSERT MEDIA*/,
                //List<Feedback> /*INSERT FEEDBACK HERE*/, List<UserSessie> /*INSERT AANWEZIGEN HIER*/);
                );

            December = new List<Sessie>
            {
                sessie4,
                sessie5
            };

            Januari = new List<Sessie>();

            Februari = new List<Sessie>
            {
                sessie1,
                sessie2,
                sessie3
            };
        }
    }
}
