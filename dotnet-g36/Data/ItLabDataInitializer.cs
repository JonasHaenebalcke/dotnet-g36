﻿using dotnet_g36.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_g36.Data
{
    public class ItLabDataInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private Verantwoordelijke admin;
        private Verantwoordelijke organizer1;
        private Verantwoordelijke organizer2;
        private Gebruiker actieveGebruiker;
        private Gebruiker nietActieveGebruiker;
        private Gebruiker geblokkeerdeGebruiker;

        public ItLabDataInitializer(ApplicationDbContext context,UserManager<IdentityUser> userManager )
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task initializeData() {
            _context.Database.EnsureDeleted();

            if (_context.Database.EnsureCreated())
            {


                // Hoofdverantwoordelijke string barcode, string username, string email, string wachtwoord, string voornaam, string familienaam, StatusGebruiker statusGebruiker = StatusGebruiker.Actief
                admin = new Verantwoordelijke("1111783544717", "862159lv", "lucas.vanderhaegen@student.hogent.be", "123", "lucas", "van der haegen", new List<Sessie>(), StatusGebruiker.Actief) { IsHoofdverantwoordelijke = true};
                //_context.Hoofdverantwoordelijken.Add(admin);
                _context.SaveChanges();
                //verantwoordelijke
                organizer1 = new Verantwoordelijke("123", "132lv", "audrey.behiels@student.hogent.be", "123", "audrey", "De SubAdmin1", new List<Sessie>(), StatusGebruiker.Actief);
                organizer2 = new Verantwoordelijke("123", "132lv", "audrey.behiels@student.hogent.be", "123", "Organiser2", "De SubAdmin2", new List<Sessie>(), StatusGebruiker.Actief);
                _context.Verantwoordelijken.Add(organizer1);
                _context.Verantwoordelijken.Add(organizer2);
                _context.SaveChanges();

                // Deelnemers
                Gebruiker actieveGebruiker = new Gebruiker("123", "132lv", "audrey.behiels@student.hogent.be", "123", "Lucifer", "De Duvel", StatusGebruiker.Actief);
                Gebruiker nietActieveGebruiker = new Gebruiker("123", "132lv", "audrey.behiels@student.hogent.be", "123", "Kim", "jansens", StatusGebruiker.NietActief);
                Gebruiker geblokkeerdeGebruiker = new Gebruiker("123", "132lv", "audrey.behiels@student.hogent.be", "123", "Tom", "Tomsens", StatusGebruiker.Geblokkeerd);
                _context.Gebruikers.AddRange(new Gebruiker[]
                {actieveGebruiker, nietActieveGebruiker, geblokkeerdeGebruiker });
                _context.SaveChanges();

                await InitializeDeelnemersEnVerantwoordelijke();
                //Sessies
                // Jan 

                //vb huidigeMaand
                Sessie huidigeMaandSessie = new Sessie(admin, organizer1, "Sessie 3D Printing", "B1.027",
                DateTime.Now, DateTime.Now.AddHours(2),
                25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                );

                //Maart - niet open
                Sessie sessie1 = new Sessie(admin, organizer1, "Sessie 3D Printing", "B1.027",
                   new DateTime(2020, 3, 1, 7, 30, 0), new DateTime(2020, 3, 1, 9, 30, 0),
                    25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                    );
                //Maart  - niet open
                Sessie sessie2 = new Sessie(admin, organizer1, "Sessie Netflix", "BCON",
                     new DateTime(2020, 3, 27, 12, 30, 0), new DateTime(2020, 3, 27, 13, 30, 0),
                    150, StatusSessie.NietOpen, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", "Jonas Haenebalcke"
                    );
                //Maart - gesloten
                Sessie sessie3 = new Sessie(admin, organizer1, "Omgaan met frustratie problemen", "B4.012",
                    new DateTime(2020, 3, 20, 12, 30, 0), new DateTime(2020, 3, 20, 13, 30, 0),
                    25, StatusSessie.Gesloten, "Lucas legt in deze lezing uit hoe je moet omgaan met frustratie's uit het dagelijkse leven", "Lucas Van De Haegen"
                    );
                // Dec - niet open
                Sessie sessie4 = new Sessie(admin, organizer2, "Sessie over slechte Java", "B4.015",
                     new DateTime(2019, 12, 24, 7, 30, 0), new DateTime(2019, 12, 24, 9, 30, 0),
                    25, StatusSessie.NietOpen, "Een sessie over wat slechte Java is", " "
                    );
                //Dec - gesloten
                Sessie sessie5 = new Sessie(admin, organizer1, "Sessie Java", "BCON",
                     new DateTime(2019, 12, 27, 12, 30, 0), new DateTime(2019, 12, 27, 13, 30, 0),
                    150, StatusSessie.Gesloten, "Een lezing over Java", "Ruben Ruby"
                    );
                // Jan - open
                Sessie sessie6 = new Sessie(admin, organizer1, "Sessie DotNet", "BCON",
                     new DateTime(2020, 3, 1, 12, 30, 0), new DateTime(2019, 03, 1, 13, 30, 0),
                    150, StatusSessie.Open, " ", " "
                    );
                // Feb - niet open
                Sessie sessie7 = new Sessie(admin, null, "Infosessie Visual Studio", "B1.012",
                    new DateTime(2019, 2, 12, 12, 30, 0), new DateTime(2019, 3, 12, 13, 30, 0), 150,
                    StatusSessie.NietOpen, "Alle nodige info over Visual Studio voor dit semster", "Stefaan De Cock");
                // Feb - gesloten
                Sessie sessie8 = new Sessie(admin, null, "Infosessie Visual Studio Code", "B1.013",
                  new DateTime(2019, 2, 19, 12, 30, 0), new DateTime(2019, 3, 19, 13, 30, 0), 30,
                  StatusSessie.Gesloten, "Alle nodige info over Visual Studio Code voor dit semster", " ");
                // Feb - gesloten
                Sessie sessie9 = new Sessie(admin, organizer2, "Infosessie Eclipse", "B1.013",
                 new DateTime(2019, 2, 26, 12, 30, 0), new DateTime(2019, 3, 26, 13, 30, 0), 25,
                 StatusSessie.Gesloten, "Alle nodige info om met Eclipe te werken", " ");
                //Mei - niet open
                Sessie sessie10 = new Sessie(admin, organizer2, "Sessie MySQL", "B4.013",
                 new DateTime(2020, 5, 5, 12, 30, 0), new DateTime(2020, 5, 5, 13, 30, 0), 50,
                 StatusSessie.NietOpen, "Sessie over MySQL", " ");

                admin.OpenTeZettenSessies = new List<Sessie>() { sessie1, sessie2, sessie3, sessie4, sessie5, sessie6, sessie6, sessie7, sessie8, sessie9, sessie10 };
                organizer1.OpenTeZettenSessies = new List<Sessie>() { sessie1,sessie2, sessie3, sessie5, sessie6};
                organizer2.OpenTeZettenSessies = new List<Sessie>() { sessie4, sessie9, sessie10};

                _context.Sessies.AddRange(new Sessie[]
              {
                    sessie1, sessie2, sessie3, sessie4, sessie5, sessie6, sessie6, sessie7, sessie8, sessie9, sessie10
              });
                _context.SaveChanges();

            }
        }
        private async Task InitializeDeelnemersEnVerantwoordelijke()
        {
            await _userManager.CreateAsync(admin, "123");
            await _userManager.AddClaimAsync(admin, new Claim(ClaimTypes.Role, "Hoofdverantwoordelijke"));
            //string eMailAddress = "Hoofdverantwoordelijke@hogent.be";
            //User user = new User { UserName = "Hfdverantwoordelijke", Email = eMailAddress };
            //await _userManager.CreateAsync(user, "1234");
            //await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Hoofdverantwoordelijke"));


         //   eMailAddress = "Student1@hogent.be";
         //user = new User { UserName = "student1", Email = eMailAddress };
        await _userManager.CreateAsync(organizer1, "123");
        await _userManager.AddClaimAsync(organizer1, new Claim(ClaimTypes.Role, "Verantwoordelijke"));

            await _userManager.CreateAsync(actieveGebruiker, "123");
            await _userManager.AddClaimAsync(actieveGebruiker, new Claim(ClaimTypes.Role, "Deelnemer"));
        }
    }
}
    

