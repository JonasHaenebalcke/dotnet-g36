using dotnet_g36.Models.Domain;
using dotnet_g36.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
            IEnumerable<Sessie> sessies = new List<Sessie>();
            Verantwoordelijke gebruiker = (Verantwoordelijke)_userRepository.GetDeelnemerByUsername(User.Identity.Name);
            //Vult sessies op met gepaste sessies
            if (gebruiker.Equals(_userRepository.GetHoofdverantwoordelijke()))
            {
                sessies = _sessieRepository.GetAll();
            }
            else
            {
                foreach (Sessie s in gebruiker.OpenTeZettenSessies)
                {
                    // if (s.Verantwoordelijke != null && s.Verantwoordelijke.Equals(gebruiker))

                    sessies.ToList().Add(s);

                }
            }


            //if (sessies.Count().Equals(0))
            //{
            //    throw new GeenSessiesException("Er zijn geen sessies voor de gekozen maand. Kies een andere periode.");
            //}

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