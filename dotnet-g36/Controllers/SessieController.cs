using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
            IEnumerable<Sessie> sessies = _sessieRepository.GetAll();
            //    ViewBag.DropDownList = EnumHelper.SelectListFor(EnumHelper);
            return View(sessies);
        }

    }

}