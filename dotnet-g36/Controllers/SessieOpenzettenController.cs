using System;
using System.Collections.Generic;
using dotnet_g36.Filters;
using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using dotnet_g36.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_g36.Controllers
{
    public class SessieOpenzettenController : Controller
    {
        private readonly ISessieRepository _sessieRepository;
        private readonly IGebruikerRepository _gebruikerRepository;

        public SessieOpenzettenController(ISessieRepository sessieRepository, IGebruikerRepository gebruikerRepository)
        {
            _sessieRepository = sessieRepository;
            _gebruikerRepository = gebruikerRepository;
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

                if (sessie.StatusSessie == StatusSessie.Open && DateTime.Now < sessie.StartDatum)
                    return RedirectToAction("MeldAanwezig", "MeldAanwezig", new { @id = id });

                sessie.SessieOpenZetten(verantwoordelijke);
                _sessieRepository.SaveChanges();
                return RedirectToAction("MeldAanwezig", "MeldAanwezig", new { @id = id });
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

                return RedirectToAction("Index", "Sessie");
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