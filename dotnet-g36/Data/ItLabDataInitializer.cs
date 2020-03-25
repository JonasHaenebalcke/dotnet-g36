using dotnet_g36.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_g36.Data
{
    public class ItLabDataInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Gebruiker> _userManager;
        private Verantwoordelijke admin;
        private Verantwoordelijke organizer1;
        private Verantwoordelijke organizer2;
        private Gebruiker gebruiker;
        // private Gebruiker nietActieveGebruiker;
        // private Gebruiker geblokkeerdeGebruiker;

        public ItLabDataInitializer(ApplicationDbContext context, UserManager<Gebruiker> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _context.Database.EnsureDeleted();

            if (_context.Database.EnsureCreated())
            {

                var ph = new PasswordHasher<Gebruiker>();
                // Hoofdverantwoordelijke string barcode, string username, string email, string wachtwoord, string voornaam, string familienaam, StatusGebruiker statusGebruiker = StatusGebruiker.Actief
                admin = new Verantwoordelijke("1111783544717", "862159lv", "lucas.vanderhaegen@student.hogent.be", "Lucas", "Van Der Haegen", new List<Sessie>(), StatusGebruiker.Actief)
                {
                    IsHoofdverantwoordelijke = true
                };
                admin.EmailConfirmed = true;
                admin.PasswordHash = ph.HashPassword(admin, "123");
                admin.SecurityStamp = Guid.NewGuid().ToString();
                //verantwoordelijke
                organizer1 = new Verantwoordelijke("1138622502790", "860443ab", "audrey.behiels@student.hogent.be", "Audrey", "De SubAdmin1", new List<Sessie>(), StatusGebruiker.Actief);
                organizer2 = new Verantwoordelijke("123", "860444jh", "jonas.haenebalcke@student.hogent.be", "Organiser2", "De SubAdmin2", new List<Sessie>(), StatusGebruiker.Actief);
                //organizer2 = new Verantwoordelijke("123", "860444jh", "jonas.haenebalcke@student.hogent.be", "Organiser2", "De SubAdmin2", new List<Sessie>(), StatusGebruiker.NietActief);
                //organizer2 = new Verantwoordelijke("123", "860444jh", "jonas.haenebalcke@student.hogent.be", "Organiser2", "De SubAdmin2", new List<Sessie>(), StatusGebruiker.Geblokkeerd);
                organizer1.EmailConfirmed = true;
                organizer1.PasswordHash = ph.HashPassword(organizer1, "123");
                organizer1.SecurityStamp = Guid.NewGuid().ToString();
                organizer2.EmailConfirmed = true;
                organizer2.PasswordHash = ph.HashPassword(organizer2, "123");
                organizer2.SecurityStamp = Guid.NewGuid().ToString();

                _context.Verantwoordelijken.Add(admin);

                _context.Verantwoordelijken.Add(organizer1);
                _context.Verantwoordelijken.Add(organizer2);
                // Deelnemers
                gebruiker = new Gebruiker("456", "752460rd", "rein.daelman@student.hogent.be", "Rein", "Daelman", StatusGebruiker.Actief);
                //gebruiker = new Gebruiker("456", "752460rd", "rein.daelman@student.hogent.be", "Rein", "Daelman", StatusGebruiker.NietActief);
                //gebruiker = new Gebruiker("456", "752460rd", "rein.daelman@student.hogent.be", "Rein", "Daelman", StatusGebruiker.Geblokkeerd);
                gebruiker.EmailConfirmed = true;
                gebruiker.PasswordHash = ph.HashPassword(gebruiker, "123");
                gebruiker.SecurityStamp = Guid.NewGuid().ToString();

                _context.Gebruikers.AddRange(new Gebruiker[]
                {gebruiker  });
                //nietActieveGebruiker, geblokkeerdeGebruiker
                _context.SaveChanges();
                _context.SaveChanges();
                await InitializeDeelnemersEnVerantwoordelijke();
                

                //Media
                Media media1 = new Media("Nuttige link", "https://www.w3schools.com/tags/att_a_target.asp", "Link naar W3Schools");
                Media media2 = new Media("C# Tutorial", "https://www.youtube.com/watch?v=gfkTfcpWqAY", "C# uitgelegd in een video");
                Media media3 = new Media("Algemene Vragen", "/Files/DOCX/Algemene Vragen.docx", "Word bestand van algemene vragen");
                Media media4 = new Media("Algemene Vragen", "/Files/PDF/Algemene Vragen.pdf", "Pdf bestand van algemene vragen");
                Media media5 = new Media("Algemene Vragen", "/Files/PPTX/Algemene Vragen.pptx", "Poweroint bestand van algemene vragen");
                List<Media> mediaList = new List<Media>() { media1, media2, media3, media4, media5 };
                // Media media3 = new Media("");
                
                //Sessies

                // huidigeMaand Nietopen Organizer1
                Sessie huidigeMaandSessie = new Sessie(/*admin,*/ organizer1, "Sessie Java", "B1.027",
                DateTime.Now.AddHours(1), DateTime.Now.AddHours(3),
                25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                );

                // huidigeMaand Nietopen Admin
                Sessie huidigeMaandSessieAdmin = new Sessie(admin, /*organizer1,*/ "Sessie DotNet", "B1.027",
                DateTime.Now.AddHours(1), DateTime.Now.AddHours(3),
                25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                );

                // huidigeMaand NietOpen Admin DirectSluiten
                Sessie huidigeMaandSessieSluiten = new Sessie(admin, /*organizer1,*/ "Omgaan met frustratie problemen", "B1.027",
                DateTime.Now.AddSeconds(90), DateTime.Now.AddHours(3),
                25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                );

                // Dec - 1e gesloten
                Sessie sessie4 = new Sessie(/*admin,*/ organizer2, "Sessie over slechte Java", "B4.015",
                     new DateTime(2019, 12, 24, 7, 30, 0), new DateTime(2019, 12, 24, 9, 30, 0),
                    25, StatusSessie.Gesloten, "Een sessie over wat slechte Java is", " "
                    );


                //Dec - 2e gesloten
                Sessie sessie5 = new Sessie(/*admin,*/ organizer1, "Sessie Java", "BCON",
                     new DateTime(2019, 12, 27, 12, 30, 0), new DateTime(2019, 12, 27, 13, 30, 0),
                    150, StatusSessie.Gesloten, "Een lezing over Java", "Ruben Ruby"
                    );

                //// Jan - open
                //Sessie sessie6 = new Sessie(/*admin,*/ organizer1, "Sessie DotNet", "BCON",
                //     new DateTime(2020, 3, 1, 12, 30, 0), new DateTime(2019, 03, 1, 13, 30, 0),
                //    150, StatusSessie.Open, " ", " "
                //    );

                // Feb - gesloten admin
                Sessie sessie7 = new Sessie(admin, "Infosessie Visual Studio", "B1.012",
                    new DateTime(2019, 2, 12, 12, 30, 0), new DateTime(2019, 3, 12, 13, 30, 0), 150,
                    StatusSessie.Gesloten, "Alle nodige info over Visual Studio voor dit semster", "Stefaan De Cock");
                // Feb - gesloten admin
                Sessie sessie8 = new Sessie(admin, "Infosessie Visual Studio Code", "B1.013",
                  new DateTime(2019, 2, 19, 12, 30, 0), new DateTime(2019, 3, 19, 13, 30, 0), 30,
                  StatusSessie.Gesloten, "Alle nodige info over Visual Studio Code voor dit semster", " ");
                // Feb - niet open organizer2
                Sessie sessie9 = new Sessie(/*admin,*/ organizer2, "Infosessie Eclipse", "B1.013",
                 new DateTime(2019, 2, 26, 12, 30, 0), new DateTime(2019, 3, 26, 13, 30, 0), 25,
                 StatusSessie.NietOpen, "Alle nodige info om met Eclipe te werken", " ");

                //Maart - gesloten Verleden organizer1
                Sessie sessie1 = new Sessie(/*admin,*/ organizer1, "Sessie 3D Printing", "B1.027",
                   new DateTime(2020, 3, 1, 7, 30, 0), new DateTime(2020, 3, 1, 9, 30, 0),
                    25, StatusSessie.Gesloten, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock", "Stefaan De Cock"
                    );

                sessie1.Media = mediaList;


                //Maart - niet open toekomst organizer1
                Sessie sessie2 = new Sessie(/*admin,*/ organizer1, "Sessie Netflix", "BCON",
                   new DateTime(2020, 3, 27, 12, 30, 0), new DateTime(2020, 3, 27, 13, 30, 0),
                  150, StatusSessie.NietOpen, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", "Jonas Haenebalcke"
                  );
                //Maart - gesloten organizer1
                Sessie sessie3 = new Sessie(/*admin,*/ organizer1, "Omgaan met frustratie problemen", "B4.012",
                    new DateTime(2020, 3, 20, 12, 30, 0), new DateTime(2020, 3, 20, 13, 30, 0),
                    25, StatusSessie.NietOpen, "Lucas legt in deze lezing uit hoe je moet omgaan met frustratie's uit het dagelijkse leven", "Lucas Van De Haegen"
                    );

                //Mei - 1e niet open organizer2
                Sessie sessie10 = new Sessie(/*admin,*/ organizer2, "Sessie MySQL", "B4.013",
                 new DateTime(2020, 5, 5, 12, 30, 0), new DateTime(2020, 5, 5, 13, 30, 0), 50,
                 StatusSessie.NietOpen, "Sessie over MySQL", " ");
                //Mei - 2e niet open organizer2
                Sessie sessie11 = new Sessie(/*admin,*/ organizer2, "Sessie Databanken", "BCON",
                    new DateTime(2020, 05, 10, 12, 30, 0), new DateTime(2020, 05, 10, 12, 45, 0), 50,
                    StatusSessie.NietOpen, "Databanken enzo", "De Data Expert");


                _context.Sessies.AddRange(new Sessie[]
              {
                   huidigeMaandSessie, sessie1, sessie2, sessie3, sessie4, sessie5, /*sessie6,*/ sessie7, sessie8, sessie9, sessie10, sessie11,
                   huidigeMaandSessieAdmin, huidigeMaandSessieSluiten
              });
                _context.SaveChanges();

                //admin.OpenTeZettenSessies = new List<Sessie>() { huidigeMaandSessie, sessie1, sessie2, sessie3, sessie4, sessie5, sessie6, sessie6, sessie7, sessie8, sessie9, sessie10, sessie11 };
                //organizer1.OpenTeZettenSessies = new List<Sessie>() { huidigeMaandSessie, sessie1, sessie2, sessie3, sessie5/*, sessie6*/ };
                //organizer2.OpenTeZettenSessies = new List<Sessie>() { sessie4, sessie9, sessie10, sessie11};
                organizer1.OpenTeZettenSessies = new List<Sessie>() { huidigeMaandSessie, sessie1, sessie2, sessie3, sessie5 };
                organizer2.OpenTeZettenSessies = new List<Sessie>() { sessie4, sessie9, sessie10, sessie11 };
                _context.SaveChanges();

                //UserSessie us1 = new UserSessie(sessie1, actieveGebruiker);

                huidigeMaandSessie.SchrijfIn(organizer1);
                huidigeMaandSessieAdmin.SchrijfIn(admin);
                huidigeMaandSessieSluiten.SchrijfIn(organizer1);
                huidigeMaandSessieSluiten.SchrijfIn(admin);
                huidigeMaandSessieSluiten.SchrijfIn(organizer2);
                //UserSessie us7 = new UserSessie(huidigeMaandSessie, organizer2);
                huidigeMaandSessieSluiten.SchrijfIn(gebruiker);
                //UserSessie us8 = new UserSessie(huidigeMaandSessie, gebruiker);
                GebruikerSessie us4 = new GebruikerSessie(sessie7, gebruiker) { Aanwezig = true };
                sessie7.GebruikerSessies.Add(us4);
                sessie10.SchrijfIn(gebruiker);
                GebruikerSessie us6 = new GebruikerSessie(sessie4, gebruiker) { Aanwezig = true };
                sessie4.GebruikerSessies.Add(us6);

                GebruikerSessie us8 = new GebruikerSessie(sessie4, organizer2) { Aanwezig = true };
                sessie4.GebruikerSessies.Add(us8);
                GebruikerSessie us9 = new GebruikerSessie(sessie5, organizer1);
                sessie5.GebruikerSessies.Add(us9);
                //UserSessie us10 = new UserSessie(sessie6, organizer1);
                GebruikerSessie us11 = new GebruikerSessie(sessie7, admin);
                sessie7.GebruikerSessies.Add(us11);
                GebruikerSessie us12 = new GebruikerSessie(sessie8, admin);
                sessie8.GebruikerSessies.Add(us12);
                GebruikerSessie us13 = new GebruikerSessie(sessie9, organizer2);
                sessie9.GebruikerSessies.Add(us13);
                GebruikerSessie us14 = new GebruikerSessie(sessie10, organizer2);
                sessie10.GebruikerSessies.Add(us14);
                GebruikerSessie us15 = new GebruikerSessie(sessie11, organizer2);
                sessie11.GebruikerSessies.Add(us15);
                GebruikerSessie us16 = new GebruikerSessie(sessie1, organizer1);
                sessie1.GebruikerSessies.Add(us16);
                GebruikerSessie us17 = new GebruikerSessie(sessie2, organizer1);
                sessie2.GebruikerSessies.Add(us17);
                GebruikerSessie us18 = new GebruikerSessie(sessie3, organizer1);
                sessie3.GebruikerSessies.Add(us18);

                //foreach (UserSessie uc in sessie7.UserSessies)
                //{
                //    if (uc.UserID == gebruiker.Id)
                //        uc.Aanwezig = true;
                //}

                //_context.SaveChanges();

                _context.GebruikerSessies.AddRange(new GebruikerSessie[]{
                    us4, us6, us8, us9, us11, us12, us13, us14, us15, us16, us17, us18
                });
                _context.SaveChanges();

                sessie4.FeedbackGeven("Intressante sessie", gebruiker, 4);//gebruiker, "Intressante sessie", DateTime.Now.AddDays(-4));
                //sessie4.FeedbackGeven("yeet", organizer2);//admin, "yeet", DateTime.Now);

                //_context.Feedbacks.AddRange(feedback1, feedback2);
                _context.SaveChanges();
            }
        }
        private async Task InitializeDeelnemersEnVerantwoordelijke()
        {

            // await _userManager.CreateAsync(admin, "123");
            await _userManager.AddClaimAsync(admin, new Claim(ClaimTypes.Role, "Hoofdverantwoordelijke"));


            //await _userManager.CreateAsync(organizer1, "123");
            await _userManager.AddClaimAsync(organizer1, new Claim(ClaimTypes.Role, "Verantwoordelijke"));

            // await _userManager.CreateAsync(organizer2, "123");
            await _userManager.AddClaimAsync(organizer2, new Claim(ClaimTypes.Role, "Verantwoordelijke"));

            // await _userManager.CreateAsync(actieveGebruiker, "123");
            await _userManager.AddClaimAsync(gebruiker, new Claim(ClaimTypes.Role, "Deelnemer"));
        }
    }
}


