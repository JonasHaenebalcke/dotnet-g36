using dotnet_g36.Models.Domain;
using dotnet_g36.Tests.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace dotnet_g36.Tests.Models.Domain
{
    public class HoofdverantwoordelijkeTest
    {
        private Hoofdverantwoordelijke _hoofdverantwoordelijke;
        private readonly DummyDbContext _context;

        private List<Sessie> AlleSessiesLijst;

        // test constructor
        public HoofdverantwoordelijkeTest()
        {

            _context = new DummyDbContext();
            AlleSessiesLijst = new List<Sessie>();

        }

        // geen sessies om open te zetten
        [Fact]
        public void SessieOpenZetten_geenSessiesOmOpenTeZetten_melding()
        {
            //Assert.Null(_hoofdverantwoordelijke.AlleSessies);
            Assert.Equal(new List<Sessie>(), _hoofdverantwoordelijke.AlleSessies);
        }

        [Fact]
        public void SessieOpenZetten_1SessieOpenZetten_gelukt()
        {
            // Sessie sessie = new Sessie(5, admin, organizer, "Bad Test Driven Development", "Mark Koekens", )
           
            AlleSessiesLijst.Add(_context.SessieHoofd);
        //   _context.SessieHoofd.
           // Assert.Equal(1, _hoofdverantwoordelijke.AlleSessies.);
        }




    }
}
