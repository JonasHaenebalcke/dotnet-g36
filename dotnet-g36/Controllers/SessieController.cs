using dotnet_g36.Filters;
using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using dotnet_g36.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_g36.Controllers
{
    public class SessieController : Controller
    {
        private readonly ISessieRepository _sessieRepository;
        private readonly IGebruikerRepository _gebruikerRepository;

        public SessieController(ISessieRepository sessieRepository, IGebruikerRepository gebruikerRepository)
        {
            _sessieRepository = sessieRepository;
            _gebruikerRepository = gebruikerRepository;
        }

        /// <summary>
        /// Geeft de sessies van de gekozen maand
        /// </summary>
        /// <param name="maandNr">idnummer van de gekozen maand [default maand = 0]</param>
        /// <returns>View naar kalender van sessies</returns>
        [AllowAnonymous]
        [ServiceFilter(typeof(GebruikerFilter))]
        public IActionResult Index(Gebruiker gebruiker, int maandNr = 0)
        {
            try
            {
                if (maandNr == 0)
                {
                    maandNr = DateTime.Now.Month;
                }

                IEnumerable<Sessie> sessies = _sessieRepository.GetByMonth(maandNr);
                if (sessies.Count() == 0)
                    throw new SessieException("Er zijn geen sessies voor de gekozen maand. Kies een andere periode.");
                return View(new SessieKalenderViewModel(sessies, gebruiker, maandNr));
            }
            catch (SessieException gse)
            {
                TempData["error"] = gse.Message;
                return View(new SessieKalenderViewModel(new List<Sessie>(), gebruiker, maandNr));
            }
        }

        /// <summary>
        /// Geeft de details van de gekozen sessie weer
        /// </summary>
        /// <param name="id">idnummer van de gekozen sessie</param>
        /// <returns>View naar nieuwe pagina</returns>
        [AllowAnonymous]
        [ServiceFilter(typeof(GebruikerFilter))]
        public IActionResult Detail(Gebruiker gebruiker, int id)
        {
            Sessie sessie = _sessieRepository.GetByID(id);

            return View(new SessieDetailsViewModel(sessie, gebruiker, _gebruikerRepository.GetHoofdverantwoordelijke()));
        }


        /// <summary>
        /// De post van Detail Action als de gebruiker zich inschrijft/uitschrijft
        /// </summary>
        /// <param name="id">idnummer van de gekozen sessie</param>
        /// <param name="sessieDetailsViewModel">sessieDetailsViewModel Object</param>
        /// <returns>View naar kalender van sessies </returns>
        [AllowAnonymous]
        [ServiceFilter(typeof(GebruikerFilter))]
        [HttpPost]
        public IActionResult DetailInschrijvenUitschrijven(Gebruiker gebruiker, int id, SessieDetailsViewModel sessieDetailsViewModel)
        {
            try
            {
                Sessie sessie = _sessieRepository.GetByID(id);

                bool succes = false;
                foreach (GebruikerSessie gs in sessie.GebruikerSessies)
                {
                    if (gs.Gebruiker.Equals(gebruiker))
                    {
                        sessie.SchrijfUit(gebruiker);
                        succes = true;
                        TempData["message"] = "Uitschrijven is gelukt";
                        _sessieRepository.SaveChanges();
                        _gebruikerRepository.SaveChanges();
                        break;
                    }
                }
                if (!succes)
                {
                    sessie.SchrijfIn(gebruiker);
                    TempData["message"] = "Inschrijven is gelukt";
                    _sessieRepository.SaveChanges();
                    _gebruikerRepository.SaveChanges();
                }
            }
            catch (ArgumentException e)
            {
                TempData["error"] = e.Message;
            }
            catch (IngeschrevenException e)
            {
                TempData["error"] = e.Message;
            }
            catch (GeenActieveGebruikerException e)
            {
                TempData["error"] = e.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// De post van Detail Action als de gebruiker feedback geeft
        /// </summary>
        /// <param name="id">idnummer van de gekozen sessie</param>
        /// <param name="sessieDetailsViewModel">sessieDetailsViewModel Object</param>
        /// <returns>View naar kalender van sessies</returns>
        [AllowAnonymous]
        [ServiceFilter(typeof(GebruikerFilter))]
        [HttpPost]
        public IActionResult DetailFeedbackGeven(Gebruiker gebruiker, int id, SessieDetailsViewModel sessieDetailsViewModel)
        {
            try
            {
                Sessie sessie = _sessieRepository.GetByID(id);
                sessie.FeedbackGeven(sessieDetailsViewModel.FeedbackContent, gebruiker);
                _sessieRepository.SaveChanges();

                TempData["message"] = "Feedback is toegevoegd!";
                return RedirectToAction(nameof(Index));
            }
            catch (AanwezigException e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["error"] = "Er liep iets fout bij het feedback geven...";
                return RedirectToAction(nameof(Index));
            }
        }


        /// <summary>
        /// Geeft View van toekomstige sessies om open te zetten
        /// </summary>
        /// <param name="id">idnummer van de gekozen sessie</param>
        /// <returns>View van toekomstige sessies om open te zetten</returns>
        [ServiceFilter(typeof(VerantwoordelijkeFilter))]
        public IActionResult Openzetten(Verantwoordelijke verantwoordelijke)
        {
            try
            {
                ICollection<Sessie> sessies = new List<Sessie>();
                //Verantwoordelijke verantwoordelijke = _gebruikerRepository.GetVerantwoordelijkeByUsername(User.Identity.Name);
                //Vult sessies op met gepaste sessies
                if (verantwoordelijke.IsHoofdverantwoordelijke)
                    sessies = _sessieRepository.GetToekomstige();
                else
                {
                    foreach (Sessie s in verantwoordelijke.OpenTeZettenSessies)
                    {
                        if (s.StatusSessie.Equals(StatusSessie.NietOpen) && DateTime.Now < s.StartDatum)
                            sessies.Add(s);
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
        /// Stelt Sessie open
        /// </summary>
        /// <param name="id">idnummer van de gekozen sessie</param>
        /// <returns>View om aanwezig te stellen</returns>
        [ServiceFilter(typeof(VerantwoordelijkeFilter))]
        [HttpPost]
        public IActionResult Openzetten(Verantwoordelijke verantwoordelijke, int id)
        {
            try
            {
                Sessie sessie = _sessieRepository.GetByID(id);
                //Verantwoordelijke verantwoordelijke = _gebruikerRepository.GetVerantwoordelijkeByUsername(User.Identity.Name);

                if (sessie.StatusSessie == StatusSessie.Open && DateTime.Now < sessie.StartDatum)
                    return RedirectToAction(nameof(MeldAanwezig), new { @id = id });

                sessie.SessieOpenZetten(verantwoordelijke);
                _sessieRepository.SaveChanges();
                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
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

                if (sessie.StartDatum <= DateTime.Now && sessie.StatusSessie != StatusSessie.Gesloten)
                    throw new SessieException("Je kan zich niet meer aanmelden in deze sessie.");

                return View(new MeldAanwezigViewModel(sessie));
            }
            catch (SessieException e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
            }
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
                if (sessie.StartDatum <= DateTime.Now && sessie.StatusSessie != StatusSessie.Gesloten)
                    throw new SessieException("Je kan zich niet meer aanmelden in deze sessie.");
                //if (model.Barcode.Contains("@"))
                //{
                //    gebruiker = _gebruikerRepository.GetDeelnemerByEmail(model.Barcode);
                //}
                //else
                //{
                    gebruiker = _gebruikerRepository.GetDeelnemerByBarcode(model.Barcode);
                //}

                sessie.MeldAanwezig(gebruiker);
                _sessieRepository.SaveChanges();
                _gebruikerRepository.SaveChanges();
                TempData["message"] = "Aanmelden is gelukt!";

                ModelState.Clear();
                return View(new MeldAanwezigViewModel(sessie));
            }
            catch (SessieException e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(Index), new { @id = id });
            }
            catch (GeenActieveGebruikerException e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
            }
            catch (IngeschrevenException e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
            }
            catch (Exception e)
            {
                TempData["Error"] = "Gebruiker kon niet worden ingeschreven";

                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
            }
        }

        /// <summary>
        /// Sluit de sessie
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View naar Kalander van sessies</returns>
        public IActionResult Sluiten(int id)
        {
            try
            {
                Sessie sessie = _sessieRepository.GetByID(id);

                sessie.SessieSluiten();
                _sessieRepository.SaveChanges();
                _gebruikerRepository.SaveChanges();
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
    }
}