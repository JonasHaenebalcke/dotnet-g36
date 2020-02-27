using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Index()
        {
            //IEnumerable<Sessie> sessies = _sessieRepository.GetAll();

            IEnumerable<Sessie> sessies = _sessieRepository.GetByMonth((Month)Enum.Parse(typeof(Month), DateTime.Now.Month.ToString()));

            IEnumerable<Month> values = Enum.GetValues(typeof(Month)).Cast<Month>();

            IEnumerable<SelectListItem> items =
                from value in values
                select new SelectListItem {
                    Text = value.ToString(),
                    Value = value.ToString(),
                    Selected = value == (Month)Enum.Parse(typeof(Month), DateTime.Now.Month.ToString())
                };

            ViewBag.months = items;
            //ViewData["months"] = new SelectList(Enum.GetValues(typeof(Month)), , Enum.GetName());
            //    ViewBag.DropDownList = EnumHelper.SelectListFor(EnumHelper);
            return View(sessies);
        }

        [HttpPost]
        public IActionResult Index(Month month)
        {
            //IEnumerable<Sessie> sessies = _sessieRepository.GetAll();
            
            IEnumerable<Sessie> sessies = _sessieRepository.GetByMonth(month);
            //    ViewBag.DropDownList = EnumHelper.SelectListFor(EnumHelper);
            return View(sessies);
        }

    }

}