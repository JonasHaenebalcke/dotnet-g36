using dotnet_g36.Models.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36.Tests.Data
{
    class DummyDbContext
    {
        public IEnumerable<Sessie> december;
        public IEnumerable<Sessie> januari;
        public IEnumerable<Sessie> huidigeMaand;
        public Verantwoordelijke admin;
        public Verantwoordelijke organizer1, organizer2, actieveVerantwoordelijke, nietActieveVerantwoordelijke, geblokkeerdeVerantwoordelijke;
        public Sessie verledenSessie, hedenSessie, toekomstSessie;
        public Gebruiker actieveGebruiker, nietActieveGebruiker, geblokkeerdeGebruiker;

        public DummyDbContext()
        {
        
            // Er is maar 1 hoofdverantwoordelijke
            admin = new Verantwoordelijke("1111783544717", "862159lv", "lucas.vanderhaegen@student.hogent.be", "Lucas", "Van Der Haegen", new List<Sessie>(), StatusGebruiker.Actief)
            {
                IsHoofdverantwoordelijke = true
            };

            // Er kunnen meerdere verantwoordelijke zijn
            organizer1 = new Verantwoordelijke("1138622502790", "860443ab", "audrey.behiels@student.hogent.be", "Audrey", "De SubAdmin1", new List<Sessie>(), StatusGebruiker.Actief);
            organizer2 = new Verantwoordelijke("123", "860444jh", "jonas.haenebalcke@student.hogent.be", "Organizer2", "De SubAdmin2", new List<Sessie>(), StatusGebruiker.Actief);
            actieveVerantwoordelijke = new Verantwoordelijke("4", "860444jh", "jonas.haenebalcke@student.hogent.be", "Organiser2", "De SubAdmin2", new List<Sessie>(), StatusGebruiker.Actief);
            nietActieveVerantwoordelijke = new Verantwoordelijke("5", "860444jh", "jonas.haenebalcke@student.hogent.be", "Organiser2", "De SubAdmin2", new List<Sessie>(), StatusGebruiker.NietActief);
            geblokkeerdeVerantwoordelijke = new Verantwoordelijke("6", "860444jh", "jonas.haenebalcke@student.hogent.be", "Organiser2", "De SubAdmin2", new List<Sessie>(), StatusGebruiker.Geblokkeerd);

            // Deelnemers
            actieveGebruiker = new Gebruiker("769","45612pd","pieter.desnieter@student.hogent.be","Pieter", "De Snieter", StatusGebruiker.Actief);
            //Gebruiker Aaron = new Gebruiker("428", "48235as", "aaron.Slaerm@student.hogent.be", "Aaron", "Slaerm", StatusGebruiker.Actief);
            //Gebruiker Lucifer = new Gebruiker("1254", "1293ld", "lucifer.deduivel@student.hogent.be", "Lucifer", "De Duvel", StatusGebruiker.Actief);
            nietActieveGebruiker = new Gebruiker("9874", "6541kj", "kim.jansens@student.hogent.be", "Kim", "jansens", StatusGebruiker.NietActief);
            geblokkeerdeGebruiker = new Gebruiker("9634", "5486tt", "tom.tomsens@student.hogent.be", "Tom", "Tomsens", StatusGebruiker.Geblokkeerd);
            
            hedenSessie = new Sessie(organizer1, "Sessie 3D Printing", "B1.027",
               DateTime.Now.AddMinutes(2), DateTime.Now.AddHours(2),
               25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
               );
            
            toekomstSessie = new Sessie(organizer1, "Sessie Netflix",  "BCON",
                 //new DateTime(2020, 3, 27, 12, 30, 0), new DateTime(2020, 3, 27, 13, 30, 0),
                 DateTime.Now.AddMonths(1), DateTime.Now.AddMonths(1).AddHours(1),
                150, StatusSessie.NietOpen, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", "Jonas Haenebalcke"
                );

            verledenSessie = new Sessie(organizer2, "Sessie 3D Printing", "B1.027",
                 //new DateTime(2019, 12, 24, 7, 30, 0),  new DateTime(2019, 12, 24, 9, 30, 0),
                 DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-1).AddHours(2),
                25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                );

            Sessie sessie5 = new Sessie(organizer1, "Sessie Netflix",  "BCON",
                 new DateTime(2019, 12, 27, 12, 30, 0),  new DateTime(2019, 12, 27, 13, 30, 0),
                150, StatusSessie.Gesloten, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", "Jonas Haenebalcke"
                );
            Sessie sessie6 = new Sessie(organizer1, "Sessie DotNet", "BCON",
                 new DateTime(2020, 03, 1, 12, 30, 0), new DateTime(2019, 03, 1, 13, 30, 0),
                150, StatusSessie.Open, " ", " "
                );

            Sessie sessie1 = new Sessie(organizer1, "Sessie 3D Printing", "B1.027",
               new DateTime(2020, 3, 01, 7, 30, 0), new DateTime(2020, 3, 01, 9, 30, 0),
                25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                );

            Sessie sessie3 = new Sessie(organizer1, "Omgaan met frustratie problemen", "B4.012",
                new DateTime(2020, 3, 20, 12, 30, 0), new DateTime(2020, 3, 20, 13, 30, 0),
                25, StatusSessie.Gesloten, "Lucas legt in deze lezing uit hoe je moet omgaan met frustratie's uit het dagelijkse leven", "Lucas Van De Haegen"
                );

            admin.OpenTeZettenSessies = new List<Sessie>() { sessie1, toekomstSessie, sessie3, verledenSessie, sessie5, sessie6, sessie6};

            organizer1.OpenTeZettenSessies = new List<Sessie>() { };
            organizer2.OpenTeZettenSessies = new List<Sessie>() { };

            december = new List<Sessie>
            {
                verledenSessie,
                sessie5
            };

            januari = new List<Sessie>();

            huidigeMaand = new List<Sessie>
            {
                sessie1,
                toekomstSessie,
                sessie3,
                sessie6,
            };
        }
    }
}
