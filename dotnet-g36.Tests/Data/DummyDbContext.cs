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
        public Verantwoordelijke admin;
        public  Verantwoordelijke organizer1, organizer2;
        public Sessie verledenSessie;
        public Sessie hedenSessie;

        public DummyDbContext()
        {
            // Er is maar 1 hfdverantwoordelijke
             admin = new Verantwoordelijke("Admin", "De Padwin", StatusGebruiker.Actief, new List<Sessie>());
           // Er kunnen veerdere verantwoordelijke zijn
             organizer1 = new Verantwoordelijke("Organiser1", "De SubAdmin1", StatusGebruiker.Actief, new List<Sessie>());
             organizer2 = new Verantwoordelijke("Organiser2", "De SubAdmin2", StatusGebruiker.Actief, new List<Sessie>());
            

            // Users <-- Deelnemers
            User Pieter = new User("Pieter", "De Snieter", StatusGebruiker.Actief);
            User Aaron = new User("Aaron", "Slaerm", StatusGebruiker.Actief);
            User Lucifer = new User("Lucifer", "De Duvel", StatusGebruiker.Actief);
            User Kim = new User("Kim", "jansens", StatusGebruiker.NietActief);
            User Tom = new User("Tom", "Tomsens", StatusGebruiker.Geblokkeerd);

            IEnumerable<User> ActieveDeelnemers = new List<User>
            {
                Pieter,
                Aaron,
                Lucifer
            };
            IEnumerable<User> GeenDeelnemers = new List<User>
            {
                Kim, Tom
            };

            // Ik denk ook dat er extra attribuut moet komen voor ingeschreven
            Sessie sessie1 = new Sessie(admin, organizer1, "Sessie 3D Printing",  "B1.027",
               new DateTime(2020, 3, 01, 7, 30, 0), new DateTime(2020, 3, 01, 9, 30, 0),
                25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                );

            Sessie sessie2 = new Sessie(admin, organizer1, "Sessie Netflix",  "BCON",
                 new DateTime(2020, 3, 27, 12, 30, 0), new DateTime(2020, 3, 27, 13, 30, 0),
                150, StatusSessie.NietOpen, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", "Jonas Haenebalcke"
                );

            Sessie sessie3 = new Sessie(admin, organizer1, "Omgaan met frustratie problemen",  "B4.012",
                new DateTime(2020, 3, 20, 12, 30, 0),  new DateTime(2020, 3, 20, 13, 30, 0),
                25, StatusSessie.Gesloten,  "Lucas legt in deze lezing uit hoe je moet omgaan met frustratie's uit het dagelijkse leven", "Lucas Van De Haegen"
                );

            Sessie sessie4 = new Sessie(admin, organizer2, "Sessie 3D Printing", "B1.027",
                 new DateTime(2019, 12, 24, 7, 30, 0),  new DateTime(2019, 12, 24, 9, 30, 0),
                25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                );

            Sessie sessie5 = new Sessie(admin, organizer1, "Sessie Netflix",  "BCON",
                 new DateTime(2019, 12, 27, 12, 30, 0),  new DateTime(2019, 12, 27, 13, 30, 0),
                150, StatusSessie.Gesloten, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", "Jonas Haenebalcke"
                );
            Sessie sessie6 = new Sessie(admin, organizer1, "Sessie DotNet", "BCON",
                 new DateTime(2020, 03, 1, 12, 30, 0), new DateTime(2019, 03, 1, 13, 30, 0),
                150, StatusSessie.Open, " ", " "
                );
            verledenSessie = sessie4;
            hedenSessie = sessie2;

            admin.OpenTeZettenSessies = new List<Sessie>() { sessie1, sessie2, sessie3, sessie4, sessie5, sessie6, sessie6, /*sessie7, sessie8, sessie9, sessie10*/ };

            organizer1.OpenTeZettenSessies = new List<Sessie>() { };
            organizer2.OpenTeZettenSessies = new List<Sessie>() { };

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
                sessie3,
                sessie6,
            };
        }
    }
}
