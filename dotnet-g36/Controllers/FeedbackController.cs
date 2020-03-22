using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_g36.Filters;
using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using dotnet_g36.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_g36.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ISessieRepository _sessieRepository;

        public FeedbackController(ISessieRepository sessieRepository, IGebruikerRepository gebruikerRepository)
        {
            _sessieRepository = sessieRepository;
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
                //  return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Sessie");

            }
            catch (AanwezigException e)
            {
                TempData["error"] = e.Message;
                //   return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Sessie");
            }
            catch (Exception e)
            {
                TempData["error"] = "Er liep iets fout bij het feedback geven...";
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Sessie");
            }
        }

    }
}