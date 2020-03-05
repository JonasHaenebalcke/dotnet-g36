using dotnet_g36.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data
{
    public class ItLabDataInitializer
    {
        private readonly ApplicationDbContext _context;

        public ItLabDataInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void initializeData() {
            _context.Database.EnsureDeleted();

            if (_context.Database.EnsureCreated())
            {
                // Hoofdverantwoordelijke
                Hoofdverantwoordelijke admin = new Hoofdverantwoordelijke("Admin", "De Padwin", StatusGebruiker.Actief, new List<Sessie>());
                _context.Hoofdverantwoordelijken.Add(admin);
                _context.SaveChanges();
                //verantwoordelijke
                Verantwoordelijke organizer1 = new Verantwoordelijke("Organiser1", "De SubAdmin1", StatusGebruiker.Actief, new List<Sessie>());
                Verantwoordelijke organizer2 = new Verantwoordelijke("Organiser2", "De SubAdmin2", StatusGebruiker.Actief, new List<Sessie>());
                _context.Verantwoordelijken.Add(organizer1);
                _context.Verantwoordelijken.Add(organizer2);
                _context.SaveChanges();

                // Deelnemers
                Deelnemer user1 = new Deelnemer("Pieter", "De Snieter", StatusGebruiker.Actief);
                Deelnemer user2 = new Deelnemer("Aaron", "Slaerm", StatusGebruiker.Actief);
                Deelnemer user3 = new Deelnemer("Lucifer", "De Duvel", StatusGebruiker.Actief);
                Deelnemer user4 = new Deelnemer("Kim", "jansens", StatusGebruiker.NietActief);
                Deelnemer user5 = new Deelnemer("Tom", "Tomsens", StatusGebruiker.Geblokkeerd);
                Deelnemer user6 = new Deelnemer("Jan", "Van Den Hoge", StatusGebruiker.Actief);
                _context.Deelnemers.AddRange(new Deelnemer[]
                {
                    user1, user2, user3, user4, user5, user6
                });
                _context.SaveChanges();

                //Sessies
                // Ik denk ook dat er extra attribuut moet komen voor ingeschreven
                //Maart
                Sessie sessie1 = new Sessie(admin, organizer1, "Sessie 3D Printing", "B1.027",
                   new DateTime(2020, 3, 1, 7, 30, 0), new DateTime(2020, 3, 1, 9, 30, 0),
                    25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                    );
                //Maart
                Sessie sessie2 = new Sessie(admin, organizer1, "Sessie Netflix", "BCON",
                     new DateTime(2020, 3, 27, 12, 30, 0), new DateTime(2020, 3, 27, 13, 30, 0),
                    150, StatusSessie.NietOpen, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", "Jonas Haenebalcke"
                    );
                //Maart
                Sessie sessie3 = new Sessie(admin, organizer1, "Omgaan met frustratie problemen", "B4.012",
                    new DateTime(2020, 3, 20, 12, 30, 0), new DateTime(2020, 3, 20, 13, 30, 0),
                    25, StatusSessie.Gesloten, "Lucas legt in deze lezing uit hoe je moet omgaan met frustratie's uit het dagelijkse leven", "Lucas Van De Haegen"
                    );
                // Dec
                Sessie sessie4 = new Sessie(admin, organizer2, "Sessie over slechte Java", "B4.015",
                     new DateTime(2019, 12, 24, 7, 30, 0), new DateTime(2019, 12, 24, 9, 30, 0),
                    25, StatusSessie.NietOpen, "Een sessie over wat slechte Java is",  " "
                    );
                //Dec
                Sessie sessie5 = new Sessie(admin, organizer1, "Sessie Java", "BCON",
                     new DateTime(2019, 12, 27, 12, 30, 0), new DateTime(2019, 12, 27, 13, 30, 0),
                    150, StatusSessie.Gesloten, "Een lezing over Java" , "Ruben Ruby"
                    );
                // Jan
                Sessie sessie6 = new Sessie(admin, organizer1, "Sessie DotNet", "BCON",
                     new DateTime(2020, 3, 1, 12, 30, 0), new DateTime(2019, 03, 1, 13, 30, 0),
                    150, StatusSessie.Open, " ", " "
                    );
              
                // Feb
                Sessie sessie7 = new Sessie(admin, null, "Infosessie Visual Studio", "B1.012", 
                    new DateTime(2019, 2, 12, 12, 30, 0), new DateTime(2019, 3, 12, 13, 30, 0), 150,
                    StatusSessie.NietOpen, "Alle nodige info over Visual Studio voor dit semster", "Stefaan De Cock");
                // Feb
                Sessie sessie8 = new Sessie(admin, null, "Infosessie Visual Studio Code", "B1.013",
                  new DateTime(2019, 2, 19, 12, 30, 0), new DateTime(2019, 3, 19, 13, 30, 0), 30,
                  StatusSessie.Gesloten, "Alle nodige info over Visual Studio Code voor dit semster", " ");
                // Feb
                Sessie sessie9 = new Sessie(admin, organizer2, "Infosessie Eclipse", "B1.013",
                 new DateTime(2019, 2, 26, 12, 30, 0), new DateTime(2019, 3, 26, 13, 30, 0), 25,
                 StatusSessie.Gesloten, "Alle nodige info om met Eclipe te werken", " ");
                //Mei
                Sessie sessie10 = new Sessie(admin, organizer2, "Sessie MySQL", "B4.013",
                 new DateTime(2020, 5, 5, 12, 30, 0), new DateTime(2020, 5, 5, 13, 30, 0), 50,
                 StatusSessie.NietOpen, "Sessie over MySQL", " ");

                admin.OpenTeZettenSessies = new List<Sessie>() { sessie1, sessie2, sessie3, sessie4, sessie5, sessie6, sessie6, sessie7, sessie8, sessie9, sessie10 };
                organizer1.OpenTeZettenSessies = new List<Sessie>() { };
                organizer1.OpenTeZettenSessies = new List<Sessie>() { };

                _context.Sessies.AddRange(new Sessie[]
              {
                    sessie1, sessie2, sessie3, sessie4, sessie5, sessie6, sessie6, sessie7, sessie8, sessie9, sessie10
              });
                _context.SaveChanges();

                //sessie7.SchrijfIn(sessie7.SessieID, user1.UserID);


            }
        }
    }
}