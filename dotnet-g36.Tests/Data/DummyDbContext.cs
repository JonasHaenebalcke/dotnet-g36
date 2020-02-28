using dotnet_g36.Models.Domain;
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
        public IEnumerable<Sessie> HuidigeMaand;
        public Sessie SessieHoofd;

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

            // Ik denk ook dat er extra attribuut moet komen voor ingeschreven
            Sessie sessie1 = new Sessie(1, admin, organizer, "Sessie 3D Printing",  "B1.027",
               new DateTime(2020, 2, 24, 7, 30, 0), new DateTime(2020, 2, 24, 9, 30, 0),
                25,StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                );

            Sessie sessie2 = new Sessie(2, admin, organizer, "Sessie Netflix",  "BCON",
                 new DateTime(2020, 2, 27, 12, 30, 0), new DateTime(2020, 2, 3, 13, 30, 0),
                150, StatusSessie.NietOpen, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", "Jonas Haenebalcke"
                );

            Sessie sessie3 = new Sessie(3, admin, organizer, "Omgaan met frustratie problemen",  "B4.012",
                new DateTime(2020, 2, 20, 12, 30, 0),  new DateTime(2020, 2, 20, 13, 30, 0),
                25, StatusSessie.NietOpen,  "Lucas legt in deze lezing uit hoe je moet omgaan met frustratie's uit het dagelijkse leven", "Lucas Van De Haegen"
                );

            Sessie sessie4 = new Sessie(1, admin, organizer, "Sessie 3D Printing", "B1.027",
                 new DateTime(2019, 12, 24, 7, 30, 0),  new DateTime(2019, 12, 24, 9, 30, 0),
                25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                );

            Sessie sessie5 = new Sessie(2, admin, organizer, "Sessie Netflix",  "BCON",
                 new DateTime(2019, 12, 27, 12, 30, 0),  new DateTime(2019, 12, 3, 13, 30, 0),
                150, StatusSessie.NietOpen, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", "Jonas Haenebalcke"
                );

            SessieHoofd = sessie1;

            December = new List<Sessie>
            {
                sessie4,
                sessie5
            };

            Januari = new List<Sessie>();

            HuidigeMaand = new List<Sessie>
            {
                sessie1,
                sessie2,
                sessie3
            };
        }
    }
}
