using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using dotnet_g36.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_g36.Controllers
{
    public class MeldAanwezigController : Controller
    {

        private readonly ISessieRepository _sessieRepository;
        private readonly IGebruikerRepository _gebruikerRepository;

        public MeldAanwezigController(ISessieRepository sessieRepository, IGebruikerRepository gebruikerRepository)
        {
            _sessieRepository = sessieRepository;
            _gebruikerRepository = gebruikerRepository;
        }


        /// <summary>
        /// Om de aanwezigheden op te nemen
        /// </summary>
        /// <param name="id">idnummer van de sessie</param>
        /// <returns>View naar Aanwezigheden (aanmelden voor sessie)</returns>
        [Authorize(Roles = "Hoofdverantwoordelijke, Verantwoordelijke")]
        public IActionResult MeldAanwezig(int id)
        {
            try
            {
                Sessie sessie = _sessieRepository.GetByID(id);

                if (sessie.StartDatum <= DateTime.Now && sessie.StatusSessie != StatusSessie.Gesloten)
                    throw new SessieException("Je kan zich niet meer aanmelden in deze sessie.");

                return View(new MeldAanwezigViewModel(sessie));
            }
            catch (SessieException e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
            }
        }

        /// <summary>
        /// De Post van MeldAanwezig Action om de aanwezigheden op te nemen
        /// </summary>
        /// <param name="id">idnummer van de gekozen sessie</param>
        /// <param name="barcode">barcode van gebruiker</param>
        /// <returns>View naar aanwezigheden (aanmelden voor sessie)</returns>
        [Authorize(Roles = "Hoofdverantwoordelijke, Verantwoordelijke")]
        [HttpPost]
        public IActionResult MeldAanwezig(int id, string aanwezig, MeldAanwezigViewModel model)
        {
            try
           {
                Sessie sessie = _sessieRepository.GetByID(id);
                Gebruiker gebruiker;
                if (sessie.StartDatum <= DateTime.Now && sessie.StatusSessie != StatusSessie.Gesloten)
                    throw new SessieException("Je kan zich niet meer aanmelden in deze sessie.");
                //if (model.Barcode.Contains("@"))
                //{
                //    gebruiker = _gebruikerRepository.GetDeelnemerByEmail(model.Barcode);
                //}
                //else
                //{

                gebruiker = _gebruikerRepository.GetDeelnemerByBarcode(aanwezig);
                //}


                if (sessie.geefAlleAanwezigen().Contains(gebruiker))
                {

                    TempData["message"] = gebruiker.GeefVolledigeNaam() + " is afwezig gezet!";

                }
                else
                {
                    TempData["message"] = gebruiker.GeefVolledigeNaam() + "  is aanwezig gezet!";

                }
                sessie.MeldAanwezigAfwezig(gebruiker);
                _sessieRepository.SaveChanges();
                _gebruikerRepository.SaveChanges();

               // model.Barcode = null;
                //ModelState.Clear();
                return View(new MeldAanwezigViewModel(sessie));
            }
            catch (SessieException e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(Index), new { @id = id });
            }
            catch (GeenActieveGebruikerException e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
            }
            catch (SchrijfInSchrijfUitException e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
            }
            catch (Exception)
            {
                //  TempData["Error"] = "Gebruiker kon niet worden ingeschreven";
                TempData["Error"] = e.Message;

                return RedirectToAction(nameof(MeldAanwezig), new { @id = id });
            }
        }
    }
}