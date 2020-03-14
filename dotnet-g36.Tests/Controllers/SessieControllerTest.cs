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
        private readonly Mock<IUserRepository> _userRepo;
        private readonly int huidigeMaand;

        public SessieControllerTest()
        {
            huidigeMaand = DateTime.Now.Month;
            _context = new DummyDbContext();
            _sessieRepo = new Mock<ISessieRepository>();
            _userRepo = new Mock<IUserRepository>();

            _controller = new SessieController(_sessieRepo.Object, _userRepo.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }

        [Fact]
        public void SessieKalender_HuidigeMaand_GeeftModelMetAlle3HuidigeMaandSessieDoorAanDefaultView()
        {
            _sessieRepo.Setup(s => s.GetByMonth(huidigeMaand)).Returns(_context.HuidigeMaand);
            var actionResult = Assert.IsType<ViewResult>(_controller.Index());

            var sessies = Assert.IsAssignableFrom<IEnumerable<Sessie>>(actionResult.Model);
            Assert.Equal(4, sessies.Count());
            Assert.Equal("Sessie 3D Printing", sessies.First().Titel);
        }

        [Fact]
        public void SessieKalender_veranderDecemberMaandMet2Sessies_GeeftModelMet2DecemberSessiesDoorAanDefaultView()
        {
            _sessieRepo.Setup(s => s.GetByMonth(12)).Returns(_context.December);
            var actionResult = Assert.IsType<ViewResult>(_controller.Index(12));

            var sessies = Assert.IsAssignableFrom<IEnumerable<Sessie>>(actionResult.Model);
            Assert.Equal(2, sessies.Count());
            Assert.Equal("Sessie 3D Printing", sessies.First().Titel);
        }

        [Fact]
        public void SessieKalender_veranderJanuariMaandZonderSessies_GeeftMeldingenGeenSessiesWeer()
        {
            _sessieRepo.Setup(s => s.GetByMonth(01)).Returns(new List<Sessie>());
            var actionResult = Assert.IsType<ViewResult>(_controller.Index(01));
            var sessies = Assert.IsAssignableFrom<IEnumerable<Sessie>>(actionResult.Model);
            Assert.Empty(sessies);
        }
    }
}
