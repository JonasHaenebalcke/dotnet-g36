using dotnet_g36.Models.Domain;
using dotnet_g36.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace dotnet_g36.Controllers
{
    public class SessieOpenzettenController : Controller
    {
        private readonly ISessieRepository _sessieRepository;
        private readonly IUserRepository _userRepository;

        public SessieOpenzettenController(ISessieRepository sessieRepository, IUserRepository userRepository)
        {
            _sessieRepository = sessieRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            ICollection<Sessie> sessies = new List<Sessie>();
            Verantwoordelijke gebruiker = _userRepository.GetVerantwoordelijkeByUsername(User.Identity.Name);
            //Vult sessies op met gepaste sessies
            foreach (Sessie s in gebruiker.OpenTeZettenSessies)
            {
                if (s.StatusSessie.Equals(StatusSessie.NietOpen) && DateTime.Now <= s.StartDatum.AddHours(1))
                {
                    sessies.Add(s);
                }
            }
            return View(new SessieOpenzettenViewModel(sessies));
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            Sessie sessie = _sessieRepository.GetByID(id);
            Verantwoordelijke verantwoordelijke = _userRepository.GetVerantwoordelijke(_userRepository.GetDeelnemerByUsername(User.Identity.Name).Id);

            sessie.SessieOpenZetten(verantwoordelijke);
            _sessieRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}