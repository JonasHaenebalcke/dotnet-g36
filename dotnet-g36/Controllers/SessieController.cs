using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using dotnet_g36.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotnet_g36.Controllers
{
    public class SessieController : Controller
    {
        private readonly ISessieRepository _sessieRepository;
        private User User { get; set; } // DELETE

        public SessieController(ISessieRepository sessieRepository)
        {
            _sessieRepository = sessieRepository;
            User = new User() { UserID = 1 }; // DELETE
        }

        public IActionResult Index(int maandId = 0) //get & post
        {
            try
            {                
                //ViewData["aanwezigen"] = _sessieRepository.GetByID()
                
                if (maandId == 0)
                {
                    maandId = DateTime.Now.Month;
                }

                ViewData["maanden"] = GetMaandSelectList(maandId);

                IEnumerable<Sessie> sessies = _sessieRepository.GetByMonth(maandId);
                if (sessies.Count().Equals(0))
                {
                    throw new GeenSessiesException("Er zijn geen sessies voor de gekozen maand. Kies een andere periode.");
                }
                else
                {
                    TempData["message"] = "Er zijn sessies";
                    return View(new SessieKalenderViewModel(sessies, GetMaandSelectList(maandId), /*huidige ingelogde user*/ User)); //OPT VIEWMODEL
                    //return View(sessies);
                }
            }
            catch(GeenSessiesException gse)
            {
                TempData["error"] = gse.Message;
                return View(new List<Sessie>());
            }

        }

        
        /// <summary>
        /// Geeft de details van een sessie weer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View naar nieuwe pagina</returns>
        public IActionResult Detail(int id)
        {
            Sessie sessie = _sessieRepository.GetByID(id);

            if (sessie.Media != null)
            {
                ViewData["hasMedia"] = true;
            }
            else
            {
                ViewData["hasMedia"] = false;
            }

            if (sessie.FeedbackList != null)
            {
                ViewData["hasFeedback"] = true;
            }
            else
            {
                ViewData["hasFeedback"] = false;
            }

            //if(user is ingeschreven) {
           // ViewData["isIngeschreven"] = true;
            //}else {
            ViewData["isIngeschreven"] = false; 

            return View(new SessieDetailsViewModel(sessie, User));
        }



        /// <summary>
        /// De post van Detail Action als de gebruiker zich inschrijft
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sessieDetailsViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DetailInschrijvenUitschrijven(int id, SessieDetailsViewModel sessieDetailsViewModel)
        {
            Sessie sessie = _sessieRepository.GetByID(id);


            //UserID opvragen
            //Als user ingeschreven is, schrijf user uit
            //anders schrijf user in
            _sessieRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// De post van Detail Action als de gebruiker feedback geeft
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sessieDetailsViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DetailFeedbackGeven(int id, SessieDetailsViewModel sessieDetailsViewModel)
        {
            Sessie sessie = _sessieRepository.GetByID(id);
            

           // _sessieRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// retourneert selectlist van alle sessies in de opgegeven maand
        /// </summary>
        /// <param name="maandId">nummer van de opgegeven maand</param>
        /// <returns>selectlist van sessies</returns>
        private SelectList GetMaandSelectList(int maandId = 0)
        {
            var maanden =  DateTimeFormatInfo.CurrentInfo.MonthNames.Select((monthName, index) => new SelectListItem{Value = (index + 1).ToString(),Text = monthName});
            SelectList result = new SelectList(maanden.SkipLast(1), "Value", "Text", maandId);
            return result;
        }
    }

    

}