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
    public class InschrijvenUitschrijvenController : Controller
    {
        private readonly ISessieRepository _sessieRepository;
        private readonly IGebruikerRepository _gebruikerRepository;

        public InschrijvenUitschrijvenController(ISessieRepository sessieRepository, IGebruikerRepository gebruikerRepository)
        {
            _sessieRepository = sessieRepository;
            _gebruikerRepository = gebruikerRepository;
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

    }
}