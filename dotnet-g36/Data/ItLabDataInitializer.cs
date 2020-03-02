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
                
                //MOMENTEEL KRIJGT USER EEN ID MEE IN CONSTRUCTOR IK DENK DAT DIT WEG MOET
       

                // Hoofdverantwoordelijke
                Hoofdverantwoordelijke admin = new Hoofdverantwoordelijke("Admin", "De Padwin", StatusGebruiker.Actief);
                _context.Hoofdverantwoordelijken.Add(admin);
                _context.SaveChanges();
                //erantwoordelijke
                Verantwoordelijke organizer1 = new Verantwoordelijke("Organiser1", "De SubAdmin1", StatusGebruiker.Actief);
                Verantwoordelijke organizer2 = new Verantwoordelijke("Organiser2", "De SubAdmin2", StatusGebruiker.Actief);
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
                Sessie sessie1 = new Sessie(admin, organizer1, "Sessie 3D Printing", "B1.027",
                   new DateTime(2020, 3, 1, 7, 30, 0), new DateTime(2020, 3, 1, 9, 30, 0),
                    25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                    );

                Sessie sessie2 = new Sessie(admin, organizer1, "Sessie Netflix", "BCON",
                     new DateTime(2020, 3, 27, 12, 30, 0), new DateTime(2020, 3, 27, 13, 30, 0),
                    150, StatusSessie.NietOpen, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", "Jonas Haenebalcke"
                    );

                Sessie sessie3 = new Sessie(admin, organizer1, "Omgaan met frustratie problemen", "B4.012",
                    new DateTime(2020, 3, 20, 12, 30, 0), new DateTime(2020, 3, 20, 13, 30, 0),
                    25, StatusSessie.Gesloten, "Lucas legt in deze lezing uit hoe je moet omgaan met frustratie's uit het dagelijkse leven", "Lucas Van De Haegen"
                    );

                Sessie sessie4 = new Sessie(admin, organizer2, "Sessie 3D Printing", "B1.027",
                     new DateTime(2019, 12, 24, 7, 30, 0), new DateTime(2019, 12, 24, 9, 30, 0),
                    25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                    );

                Sessie sessie5 = new Sessie(admin, organizer1, "Sessie Netflix", "BCON",
                     new DateTime(2019, 12, 27, 12, 30, 0), new DateTime(2019, 12, 27, 13, 30, 0),
                    150, StatusSessie.Gesloten, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", "Jonas Haenebalcke"
                    );

                Sessie sessie6 = new Sessie(admin, organizer1, "Sessie DotNet", "BCON",
                     new DateTime(2020, 3, 1, 12, 30, 0), new DateTime(2019, 03, 1, 13, 30, 0),
                    150, StatusSessie.Open, " ", " "
                    );

                Sessie sessie7 = new Sessie(admin, null, "Infosessie Visual Studio", "B1.012", 
                    new DateTime(2019, 2, 15, 12, 30, 0), new DateTime(2019, 3, 15, 13, 30, 0), 150,
                    StatusSessie.NietOpen, "Alle nodige info over Visual Studio voor dit semster", "Stefaan De Cock");

                _context.Sessies.AddRange(new Sessie[]
              {
                    sessie1, sessie2, sessie3, sessie4, sessie5, sessie6, sessie6, sessie7
              });
                _context.SaveChanges();

                //sessie7.SchrijfIn(sessie7.SessieID, user1.UserID);


            }
        }
    }
}