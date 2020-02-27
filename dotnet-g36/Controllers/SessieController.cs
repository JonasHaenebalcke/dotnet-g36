using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                Month huidigeMaand = (Month)Enum.Parse(typeof(Month), DateTime.Now.Month.ToString());
                
                if (maandId == 0)
                {
                    maandId = (int)huidigeMaand;
                }
                ViewData["maanden"] = GetMaandSelectList(maandId);

                IEnumerable<Sessie> sessies = _sessieRepository.GetByMonth((Month) maandId);

                return View(sessies);
            } catch (ArgumentNullException e)
            {
                ViewData["Error"] = "Er zijn geen sessies voor de gekozen maand. Kies een andere periode.";

                return View(new List<Sessie>());
            }

        }

        private SelectList GetMaandSelectList(int maandId = 0)
        {
            var maanden = from Month m in Enum.GetValues(typeof(Month)) select new { ID = (int)m, Name = m.ToString() };
            return new SelectList(maanden, "ID", "Name", maandId);
        }
    }

}