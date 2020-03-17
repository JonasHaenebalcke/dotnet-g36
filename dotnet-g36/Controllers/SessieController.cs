using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using dotnet_g36.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
//using System.Threading.Timers;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet_g36.Controllers
{
    public class SessieController : Controller
    {
        private readonly ISessieRepository _sessieRepository;
        private readonly IUserRepository _userRepository;
        private Timer timer;

        public SessieController(ISessieRepository sessieRepository, IUserRepository userRepository)
        {
            _sessieRepository = sessieRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Timer om sessies automatisch te laten sluiten
        /// </summary>
        /// <param name="alertTime"></param>
        /// <param name="id"></param>
       /* private async Task SetUpTimer(DateTime alertTime, int id)
        {
            *//*  DateTime current = DateTime.Now;
              TimeSpan timeToGo = alertTime - current;
              if (timeToGo < TimeSpan.Zero)
              {
                  return;//time already passed
              }
              this.timer = new Timer(x =>
              {
                  //RedirectToAction(nameof(Sluiten), id);
                  //Sluiten(id);
                  Index();
              }, null, timeT
              oGo, Timeout.InfiniteTimeSpan);*//*


            Task.Delay(alertTime - DateTime.Now).ContinueWith(t => Sluiten(id));

           // await Task.Run(() => Sluiten(id, alertTime));

        }*/


        /// <summary>
        /// Sluit de sessie automatisch aan de hand van methode SetUpTimer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View naar Kalander van sessies</returns>
        public IActionResult Sluiten(int id, DateTime alertTime)
        {
            try
            {

                //Thread.Sleep(alertTime - DateTime.Now);


                Sessie sessie = _sessieRepository.GetByID(id);
                //TempData["delay"] = (int) (alertTime - DateTime.Now).TotalMilliseconds;
                sessie.SessieSluiten();

                return RedirectToAction(nameof(Index));
            }
            catch (SessieException e)
            {
                TempData["sluitenMessage"] = e.Message;
                return RedirectToAction(nameof(Openzetten));
            }
            catch (Exception e)
            {
                TempData["sluitenMessage"] = e.Message;
                return RedirectToAction(nameof(Openzetten));
            }
        }

        /// <summary>
        /// Geeft de sessies van de gekozen maand
        /// </summary>
        /// <param name="maandId">idnummer van de gekozen maand [default maand = 0]</param>
        /// <returns>View naar kalender van sessies</returns>
        [AllowAnonymous]
        public IActionResult Index(int maandId = 0)
        {
            try
            {
                Gebruiker gebruiker = _userRepository.GetDeelnemerByUsername(User.Identity.Name);

                if (maandId == 0)
                {
                    maandId = DateTime.Now.Month;
                }

                ViewData["maanden"] = GetMaandSelectList(maandId);

                IEnumerable<Sessie> sessies = _sessieRepository.GetByMonth(maandId);
                if (sessies.Count().Equals(0))
                {
                    throw new SessieException("Er zijn geen sessies voor de gekozen maand. Kies een andere periode.");
                }
                else
                {
                    return View(new SessieKalenderViewModel(sessies, GetMaandSelectList(maandId), gebruiker));
                }
            }
            catch (SessieException gse)
            {
                TempData["error"] = gse.Message;
                return View(new SessieKalenderViewModel(new List<Sessie>(), GetMaandSelectList(maandId), _userRepository.GetDeelnemerByUsername(User.Identity.Name)));
            }
        }

        /// <summary>
        /// Geeft de details van de gekozen sessie weer
        /// </summary>
        /// <param name="id">idnummer van de gekozen sessie</param>
        /// <returns>View naar nieuwe pagina</returns>
        [AllowAnonymous]
        public IActionResult Detail(int id)
        {
            Sessie sessie = _sessieRepository.GetByID(id);
            Gebruiker user = _userRepository.GetDeelnemerByUsername(User.Identity.Name);

            return View(new SessieDetailsViewModel(sessie, user));
        }


        /// <summary>
        /// De post van Detail Action als de gebruiker zich inschrijft/uitschrijft
        /// </summary>
        /// <param name="id">idnummer van de gekozen sessie</param>
        /// <param name="sessieDetailsViewModel">sessieDetailsViewModel Object</param>
        /// <returns>View naar kalender van sessies </returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult DetailInschrijvenUitschrijven(int id, SessieDetailsViewModel sessieDetailsViewModel)
        {
            try
            {
                Sessie sessie = _sessieRepository.GetByID(id);
                Gebruiker gebruiker = _userRepository.GetDeelnemerByUsername(User.Identity.Name);

                bool succes = false;
                foreach (UserSessie us in sessie.UserSessies)
                {
                    if (us.UserID.Equals(gebruiker.Id))
                    {
                        sessie.SchrijfUit(gebruiker);
                        succes = true;
                        TempData["message"] = "Uitschrijven is gelukt";
                        _sessieRepository.SaveChanges();
                        break;
                    }
                }
                if (!succes)
                {
                    sessie.SchrijfIn(gebruiker);
                    TempData["message"] = "Inschrijven is gelukt";
                    _sessieRepository.SaveChanges();
                }
                _sessieRepository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Detail), id);
            }
            catch (IngeschrevenException e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Detail), id);
            }
            catch (GeenActieveGebruikerException e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Detail), id);
            }
        }

        /// <summary>
        /// De post van Detail Action als de gebruiker feedback geeft
        /// </summary>
        /// <param name="id">idnummer van de gekozen sessie</param>
        /// <param name="sessieDetailsViewModel">sessieDetailsViewModel Object</param>
        /// <returns>View naar kalender van sessies</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult DetailFeedbackGeven(int id, SessieDetailsViewModel sessieDetailsViewModel)
        {
            Sessie sessie = _sessieRepository.GetByID(id);


            // _sessieRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Openzetten()
        {
            try
            {
                ICollection<Sessie> sessies = new List<Sessie>();
                Verantwoordelijke verantwoordelijke = _userRepository.GetVerantwoordelijkeByUsername(User.Identity.Name);
                //Vult sessies op met gepaste sessies
                if (verantwoordelijke.IsHoofdverantwoordelijke)
                    sessies = _sessieRepository.GetToekomstige();
                else
                {
                    //Sessie temp = null;
                    foreach (Sessie s in verantwoordelijke.OpenTeZettenSessies)
                    {
                        if (s.StatusSessie.Equals(StatusSessie.NietOpen) && DateTime.Now < s.StartDatum)
                        {
                            //// "sorteren" op datum
                            //if (temp == null)
                            //{
                            //    temp = s;
                            //}
                            //else
                            //{
                            //    if (temp.StartDatum <= s.StartDatum)
                            //    {
                            //        sessies.Add(temp);
                            //        temp = null;
                            //    }
                            sessies.Add(s);
                            //}
                        }
                    }
                }
                return View(new SessieOpenzettenViewModel(sessies));
            }

            catch (SessieException e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Openzetten));
            }
        }


        /// <summary>
        /// Geeft View van toekomstige sessies om open te zetten
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View van toekomstige sessies om open te zetten</returns>
        [HttpPost]
        public IActionResult Openzetten(int id)
        {
            try
            {

                Sessie sessie = _sessieRepository.GetByID(id);
                Verantwoordelijke verantwoordelijke = _userRepository.GetVerantwoordelijkeByUsername(User.Identity.Name);

                sessie.SessieOpenZetten(verantwoordelijke);
                _sessieRepository.SaveChanges();
              //  SetUpTimer(sessie.StartDatum, id);
                return RedirectToAction(nameof(MeldAanwezig), new { @id = id }); // id);
            }
            catch (SessieException e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Openzetten));
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Openzetten));
            }
        }

        /// <summary>
        /// Om de aanwezigheden op te nemen
        /// </summary>
        /// <param name="id">idnummer van de sessie</param>
        /// <returns>View naar Aanwezigheden (aanmelden voor sessie)</returns>
        [Authorize(Roles = "Hoofdverantwoordelijke, Verantwoordelijke")]
        public IActionResult MeldAanwezig(int id)
        {
            try
            {
                Sessie sessie = _sessieRepository.GetByID(id);

                if (sessie.StartDatum <= DateTime.Now)
                {
                    throw new SessieException("U kan zich niet meer aanmelden");
                    //TempData["Error"] = "U kan zich niet meer aanmelden";
                    //return RedirectToAction(nameof(Index));
                }
                ICollection<string> users = new List<string>();
                foreach (Guid aanwezige in sessie.geefAlleAanwezigen())
                {
                    users.Add(_userRepository.GetDeelnemerByID(aanwezige).UserName);
                }

                //ViewData["Aanwezig"] = sessie.geefAlleAanwezigen() as List<string>;
                //ViewData["Startdatum"] = sessie.StartDatum;
                return View(new MeldAanwezigViewModel(sessie, users));
            }
            catch (SessieException e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                //return View(new MeldAanwezigViewModel(sessie));
                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
            }
            //catch (GeenActieveGebruikerException e)
            //{
            //    TempData["Error"] = e.Message;
            //    return View();
            //}
            //catch (NietIngeschrevenException e)
            //{
            //    TempData["Error"] = e.Message;
            //    return View();
            //}
        }

        /// <summary>
        /// De Post van MeldAanwezig Action om de aanwezigheden op te nemen
        /// </summary>
        /// <param name="id">idnummer van de gekozen sessie</param>
        /// <param name="barcode">barcode van gebruiker</param>
        /// <returns>View naar aanwezigheden (aanmelden voor sessie)</returns>

        [Authorize(Roles = "Hoofdverantwoordelijke, Verantwoordelijke")]
        [HttpPost]
        public IActionResult MeldAanwezig(int id, MeldAanwezigViewModel model)
        {
            try
            {
                Sessie sessie = _sessieRepository.GetByID(id);
                Gebruiker gebruiker;
                if (sessie.StartDatum <= DateTime.Now)
                {
                    throw new SessieException("U kan zich niet meer aanmelden");
                    //TempData["Error"] = "U kan zich niet meer aanmelden";
                    //return RedirectToAction(nameof(Index));
                }
                if (model.Barcode.Contains("@"))
                {
                    gebruiker = _userRepository.GetDeelnemerByEmail(model.Barcode);
                }
                else
                {
                    gebruiker = _userRepository.GetDeelnemerByBarcode(model.Barcode);//getByBarcode in userRepository?
                }

                if (gebruiker.StatusGebruiker != StatusGebruiker.Actief)
                {
                    throw new GeenActieveGebruikerException("Gebruiker is niet actief.");
                }
                ICollection<string> users = new List<string>();
                foreach (Guid aanwezige in sessie.geefAlleAanwezigen())
                {
                    users.Add(_userRepository.GetDeelnemerByID(aanwezige).UserName);
                }

                sessie.MeldAanwezig(gebruiker);
                _sessieRepository.SaveChanges();
                TempData["message"] = "Aanmelden is gelukt!";

                //ViewData["Aanwezig"] = sessie.geefAlleAanwezigen() as List<string>;
                ModelState.Clear();
                return View(new MeldAanwezigViewModel(sessie, users));
                //return View(nameof(MeldAanwezig), id);
            }
            catch (SessieException e)
            {
                TempData["Error"] = e.Message;
                //return View(new MeldAanwezigViewModel(sessie));
                return RedirectToAction(nameof(Index), id);
            }
            catch (GeenActieveGebruikerException e)
            {
                TempData["Error"] = e.Message;
                //return View(new MeldAanwezigViewModel(sessie));
                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
            }
            catch (IngeschrevenException e)
            {
                TempData["Error"] = e.Message;
                //return View(new MeldAanwezigViewModel(sessie));
                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
            }
            catch (Exception e)
            {
                TempData["Error"] = "Gebruiker kon niet worden ingeschreven";
                //return View(new MeldAanwezigViewModel(sessie));
                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
            }
        }

        /// <summary>
        /// Retourneert selectlist van alle sessies in de opgegeven maand
        /// </summary>
        /// <param name="maandId">nummer van de opgegeven maand</param>
        /// <returns>Selectlist van sessies</returns>
        private SelectList GetMaandSelectList(int maandId = 0)
        {
            var maanden = DateTimeFormatInfo.CurrentInfo.MonthNames.Select((monthName, index) => new SelectListItem { Value = (index + 1).ToString(), Text = monthName });
            SelectList result = new SelectList(maanden.SkipLast(1), "Value", "Text", maandId);
            return result;
        }



    }
}