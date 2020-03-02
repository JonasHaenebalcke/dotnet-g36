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

        public SessieController(ISessieRepository sessieRepository)
        {
            _sessieRepository = sessieRepository;
        }

        public IActionResult Index(int maandId = 0) //get & post
        {
            try
            {                
                //Month huidigeMaand = (Month)Enum.Parse(typeof(Month), DateTime.Now.Month.ToString());
                int huidigeMaandInt = DateTime.Now.Month;
                
                if (maandId == 0)
                {
                    //maandId = (int)huidigeMaand;
                    maandId = huidigeMaandInt;
                }
                ViewData["maanden"] = GetMaandSelectList(maandId);

                //IEnumerable<Sessie> sessies = _sessieRepository.GetByMonth((Month) maandId);
                IEnumerable<Sessie> sessies = _sessieRepository.GetByMonth(maandId);
                if (sessies.Count().Equals(0))
                {
                    // Deze throw werkt nu niet meer voor effe
                    throw new GeenSessiesException("Er zijn geen sessies voor de gekozen maand. Kies een andere periode.");
                }
                else
                {
                    TempData["message"] = "Er zijn sessies";
                    return View(sessies);
                }
            }
            catch(GeenSessiesException gse)
            {
                TempData["error"] = gse.Message;
                return View(new List<Sessie>());
            }

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