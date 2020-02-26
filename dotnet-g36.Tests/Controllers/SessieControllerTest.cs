using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using dotnet_g36.Tests.Data;
using dotnet_g36.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using dotnet_g36.Controllers;

namespace dotnet_g36.Tests.Controllers
{
    public class SessieControllerTest
    {
        private readonly SessieController _controller;
        private readonly DummyDbContext _context;
        private readonly Mock<ISessieRepository> _sessieRepo;
        //private readonly SessieViewModel model;

        public SessieControllerTest()
        {
            _context = new DummyDbContext();
            _sessieRepo = new Mock<ISessieRepository>();
            _controller = new SessieController(_sessieRepo.Object){
                TempData = new Mock<ITempDataDictionary>().Object
            };
            /*model = new SessieViewModel()
            {

            };*/
        }

        //[Fact] //opt 1
        [Theory] //opt 2
        //[InlineData(3, null)]
        [InlineData(3, Month.Februari)]
        [InlineData(2, Month.December)]
        public void SessieKalender_Maand_GeeftModelMetAlleMaandSessieDoorAanDefaultView(int aantalSessies, Month maand) //opt 2
        //public void SessieKalender_HuidigeMaand_GeeftModelMetAlle3HuidigeMaandSessieDoorAanDefaultView(Month maand) //opt 1
        {
            var contextsessie = _context.GetByMonth(maand); //opt2
            _sessieRepo.Setup(s => s.GetByMonth(maand)).Returns(contextsessie); //opt 2
            //_sessieRepo.Setup(s => s.GetByMonth(maand)).Returns(_context.huidigeMaandSessies); //opt 1
            var actionResult = Assert.IsType<ViewResult>(_controller.SessieKalender(maand));
            var sessies = Assert.IsAssignableFrom<IEnumerable<Sessie>>(actionResult.Model);
            Assert.Equal(aantalSessies, sessies.Count()); //opt 2
            //Assert.Equal(3, sessies.Count()); //opt 1
        }

        //[Fact] //opt1
        //public void SessieKalender_veranderDecemberMaandMet2Sessies_GeeftModelMet2DecemberSessiesDoorAanDefaultView()
        //{
        //    _sessieRepo.Setup(s => s.GetByMonth(Month.December)).Returns(_context.huidigeMaandSessies);
        //    var actionResult = Assert.IsType<ViewResult>(_controller.SessieKalender(Month.December));
        //    var sessies = Assert.IsAssignableFrom<IEnumerable<Sessie>>(actionResult.Model);
        //    Assert.Equal(2, sessies.Count());
        //}

        [Fact] //ook in theorie ? => neen? want foutmelding?
        public void SessieKalender_veranderJanuariMaandZonderSessies_GeeftMeldingenGeenSessiesWeer()
        {
            _sessieRepo.Setup(s => s.GetByMonth(Month.Januari)).Throws<GeenSessiesException>(); //Custom Exception? //(_context.huidigeMaandSessies);
            var actionResult = Assert.IsType<ViewResult>(_controller.SessieKalender(Month.Januari));
            //var sessies = Assert.IsAssignableFrom<IEnumerable<Sessie>>(actionResult.Model);
            //Assert.Equal(0, sessies.Count());
            Assert.Null(actionResult.Model);
        }
    }
}
