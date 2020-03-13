﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using dotnet_g36.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_g36.Controllers
{
    public class SessieController : Controller
    {
        private readonly ISessieRepository _sessieRepository;
        private Gebruiker Gebruiker { get; set; } // DELETE

        public SessieController(ISessieRepository sessieRepository)
        {

            _sessieRepository = sessieRepository;
        }

        [AllowAnonymous]
        public IActionResult Index(int maandId = 0) //get & post
        {
            try
            {
                //if(userid == null || userid.Length == 0)
                //    return 
                
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
                    return View(new SessieKalenderViewModel(sessies, GetMaandSelectList(maandId))); //OPT VIEWMODEL
                }
            }
            catch(GeenSessiesException gse)
            {
                TempData["error"] = gse.Message;
                return View(new SessieKalenderViewModel( new List<Sessie>(), GetMaandSelectList(maandId)));
            }

        }


        /// <summary>
        /// Geeft de details van een sessie weer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View naar nieuwe pagina</returns>
        [AllowAnonymous]
        public IActionResult Detail(int id)
        {
            Sessie sessie = _sessieRepository.GetByID(id);

            return View(new SessieDetailsViewModel(sessie/*, Gebruiker*/));
        }



        /// <summary>
        /// De post van Detail Action als de gebruiker zich inschrijft
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sessieDetailsViewModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
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
        [AllowAnonymous]
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

        //[AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
        //public class GebruikerFilter : ActionFilterAttribute
        //{
        //    private readonly IGebruikerRepository _gebruikerRepository;

        //    public GebruikerFilter(IGebruikerRepository gebruikerRepository)
        //    {
        //        _gebruikerRepository = gebruikerRepository;
        //    }

        //    public override void OnActionExecuting(ActionExecutingContext context)
        //    {
        //        context.ActionArguments["gebruiker"] = context.HttpContext.User.Identity.IsAuthenticated ?
        //                _gebruikerRepository.GetByUsername(context.HttpContext.User.Identity.Name) : null;
        //        base.OnActionExecuting(context);
        //    }
        //}
    }

    

}