using dotnet_g36.Filters;
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
                    maandNr = DateTime.Now.Month;
                ViewData["maanden"] = GetMaandSelectList(maandNr);

                IEnumerable<Sessie> sessies = _sessieRepository.GetByMonth(maandNr);
                if (sessies.Count() == 0)
                    throw new SessieException("Er zijn geen sessies voor de gekozen maand. Kies een andere periode.");
                ICollection<SessieKalenderViewModel> res = new List<SessieKalenderViewModel>();
                foreach(Sessie sessie in sessies)
                {
                    res.Add(new SessieKalenderViewModel(sessie, gebruiker));
                }

                return View(res);
                //return View(new SessieKalenderViewModel(sessies, gebruiker, maandNr));
            }
            catch (SessieException gse)
            {
                TempData["error"] = gse.Message;
                return View(new List<SessieKalenderViewModel>());
                //return View(new SessieKalenderViewModel(new List<Sessie>(), gebruiker, maandNr));
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

            List<int> scores = new List<int>()
                {
                   0,1,2,3,4,5
                };
            SelectList scoresSelectList = new SelectList(scores);
            ViewData["scores"] = scoresSelectList;

            return View(new SessieDetailsViewModel(sessie, gebruiker));
        }

        /// <summary>
        /// Retourneert selectlist van alle sessies in de opgegeven maand
        /// </summary>
        /// <param name="maandId">nummer van de opgegeven maand</param>
        /// <returns>Selectlist van sessies</returns>
        private SelectList GetMaandSelectList(int maandId/* = 0*/)
        {
            var maanden = DateTimeFormatInfo.CurrentInfo.MonthNames.Select((monthName, index) => new SelectListItem { Value = (index + 1).ToString(), Text = monthName });
            SelectList result = new SelectList(maanden.SkipLast(1), "Value", "Text", maandId);
            return result;
        }



    }
}