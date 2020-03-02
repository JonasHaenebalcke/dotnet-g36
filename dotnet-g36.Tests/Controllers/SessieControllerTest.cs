using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using dotnet_g36.Tests.Data;
using dotnet_g36.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using dotnet_g36.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using dotnet_g36.Models.Exceptions;
using dotnet_g36.Models.Domain;

namespace dotnet_g36.Tests.Controllers
{
    public class SessieControllerTest
    {
        private readonly SessieController _controller;
        private readonly DummyDbContext _context;
        private readonly Mock<ISessieRepository> _sessieRepo;
        //private readonly Month huidigeMaand;
        private readonly int huidigeMaand;

        public SessieControllerTest()
        {
            //huidigeMaand = (Month)Enum.Parse(typeof(Month), DateTime.Now.Month.ToString());
            huidigeMaand = DateTime.Now.Month;
            _context = new DummyDbContext();
            _sessieRepo = new Mock<ISessieRepository>();
            _controller = new SessieController(_sessieRepo.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        [Fact]
        public void SessieKalender_HuidigeMaand_GeeftModelMetAlle3HuidigeMaandSessieDoorAanDefaultView()
        {
            //Month maand = (Month)Enum.Parse(typeof(Month), DateTime.Now.Month.ToString());
            //_sessieRepo.Setup(s => s.GetByMonth(maand)).Returns(_context.HuidigeMaand);
            _sessieRepo.Setup(s => s.GetByMonth(huidigeMaand)).Returns(_context.HuidigeMaand);
            var actionResult = Assert.IsType<ViewResult>(_controller.Index());

            var sessies = Assert.IsAssignableFrom<IEnumerable<Sessie>>(actionResult.Model);
            Assert.Equal(3, sessies.Count());
            Assert.Equal("Sessie 3D Printing", sessies.First().Titel);
        }

        [Fact]
        public void SessieKalender_veranderDecemberMaandMet2Sessies_GeeftModelMet2DecemberSessiesDoorAanDefaultView()
        {
            //_sessieRepo.Setup(s => s.GetByMonth(Month.December)).Returns(_context.December);
            _sessieRepo.Setup(s => s.GetByMonth(12)).Returns(_context.December);
            var actionResult = Assert.IsType<ViewResult>(_controller.Index((int)Month.December));

            var sessies = Assert.IsAssignableFrom<IEnumerable<Sessie>>(actionResult.Model);
            Assert.Equal(2, sessies.Count());
            Assert.Equal("Sessie 3D Printing", sessies.First().Titel);
        }

        [Fact]
        public void SessieKalender_veranderJanuariMaandZonderSessies_GeeftMeldingenGeenSessiesWeer()
        {
            //_sessieRepo.Setup(s => s.GetByMonth(Month.Januari)).Throws<ArgumentNullException>(); //Custom Exception? //(_context.huidigeMaandSessies);
            _sessieRepo.Setup(s => s.GetByMonth(01)).Throws<ArgumentNullException>(); //Custom Exception? //(_context.huidigeMaandSessies);
            //var actionResult = Assert.IsType<ViewResult>(_controller.Index((int)Month.Januari));
            var actionResult = Assert.IsType<ViewResult>(_controller.Index(01));
            var sessies = Assert.IsAssignableFrom<IEnumerable<Sessie>>(actionResult.Model);
            Assert.Empty(sessies);
        }

        [Fact] //geschreven door Rein, doel vd test?
        public void KiesSessieTest()
        {

        }
    }
}
