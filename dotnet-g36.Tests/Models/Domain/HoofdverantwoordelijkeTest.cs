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
        private readonly Hoofdverantwoordelijke _hoofdverantwoordelijke;
        private readonly DummyDbContext _context;
        private readonly List<Sessie> _alleSessiesLijst; // sessies worden toegevoegd
      //  private readonly List<Sessie> _sessieLijst; // sessies worden bekeken.
        //private readonly List<Sessie> _verledenSessieLijst; // sessies worden bekeken.
       private  Sessie hedenSessie, verledenSessie;
        
        // constructorTest
        public HoofdverantwoordelijkeTest()
        {
            _context = new DummyDbContext();
            _alleSessiesLijst = new List<Sessie>();
          //  _sessieLijst = (List<Sessie>) _context.HuidigeMaand;
         //   _verledenSessieLijst = (List<Sessie>)_context.December;
         
        }

        // sessies hier instaan aanpassen

        // Er is toch een verschil tussen "Er zijn geen sessies" -> 0 sessies in lijst  en 
        // "er zijn geen sessies meer die je kan open zetten" -> er staan sessies in de lijst, maar geen één die je kan open zetten
        [Fact]
        public void SessieOpenzetten_geenSessiesOmOpenTeZetten_melding()
        {
             Assert.Empty(_hoofdverantwoordelijke.OpenTeZettenSessies);
            // Assert.Throws<ArgumentNullException>(() => { return "Er zijn geen sessies om open te zetten"; });
            Assert.Throws<ArgumentNullException>(() => _hoofdverantwoordelijke.OpenTeZettenSessies);
        }

        [Fact ]
        public void SessieOpenzetten_1SessieOpenZetten_gelukt()
        {
            hedenSessie = _context.hedenSessie;
            _alleSessiesLijst.Add(hedenSessie); // Sessie toevoegen aan lijst
            _hoofdverantwoordelijke.SessieOpenZetten(hedenSessie.SessieID);
            Assert.Equal(StatusSessie.Open, hedenSessie.StatusSessie);
            // _alleSessiesLijst.Add(_sessieLijst.Find((Sessie s) => s.Equals("sessie2"))); // Sessie toevoegen aan lijst
            //_alleSessiesLijst.Add(_sessieLijst.Find(s => s.SessieID.Equals(2))); // Sessie toevoegen aan lijst
            //_hoofdverantwoordelijke.SessieOpenZetten(_sessieLijst.Find(s => s.SessieID.Equals(2)).SessieID); 
           // Assert.Equal(StatusSessie.Open, _sessieLijst.Find(s => s.SessieID.Equals(2)).StatusSessie);
        }

        [Fact]
        public void SessieOpenzetten_DieAlOpenStaat_melding()
        {
             hedenSessie = _context.hedenSessie;
            hedenSessie.StatusSessie = StatusSessie.Open;
            _alleSessiesLijst.Add(hedenSessie);
            Assert.Throws<ArgumentException>(() => { _hoofdverantwoordelijke.SessieOpenZetten(hedenSessie.SessieID); });

            //Sessie openSessie = _sessieLijst.Find(s => s.Equals("sessie6"));
            //_alleSessiesLijst.Add(openSessie);
            //Assert.False(_hoofdverantwoordelijke.SessieOpenZetten(openSessie.SessieID));
            //  Assert.Throws<ArgumentException>(() => { return "Je kan geen sessie openzetten die al open staat."; });
            // Assert.Throws<ArgumentException>(() => { _hoofdverantwoordelijke.SessieOpenZetten(openSessie.SessieID); });
        }

        [Fact] 
        public void SessieOpenzetten_dieGeslotenIs_melding()
        {
            hedenSessie = _context.hedenSessie;
            hedenSessie.StatusSessie = StatusSessie.Gesloten;
            _alleSessiesLijst.Add(hedenSessie);
            // Sessie geslotenSessie = _sessieLijst.Find(s => s.Equals("sessie3"));
          //  _alleSessiesLijst.Add(geslotenSessie);// sessie toevoegen aan lijst.
            // Assert.False(_hoofdverantwoordelijke.SessieOpenZetten(geslotenSessie.SessieID));
           //Assert.Throws<ArgumentException>(() => { return "Gesloten sessies kan je niet terug openzetten."; });
            Assert.Throws<ArgumentException>(() => { _hoofdverantwoordelijke.SessieOpenZetten(hedenSessie.SessieID); });

        }

        [Fact]
        public void SessieOpenzetten_dieInHetVerledenLigt_melding()
        {
            //Sessie sessieVerleden = _verledenSessieLijst.Find(s => s.Equals("sessie4"));
            verledenSessie = _context.verledenSessie;
            _alleSessiesLijst.Add(verledenSessie);
            Assert.NotEqual(DateTime.Now.Year, verledenSessie.StartDatum.Year);
            //  Assert.False(_hoofdverantwoordelijke.SessieOpenZetten(sessieVerleden.SessieID));
            //  Assert.Throws<ArgumentException>(() => { return "Sessie in het verleden kan je niet terug openzetten."; });
            Assert.Throws<ArgumentException>(() => { _hoofdverantwoordelijke.SessieOpenZetten(verledenSessie.SessieID); });

        }


        //[Fact]
        [Theory]
        [MemberData(nameof(TestCase.DatumIndex), MemberType= typeof(TestCase))]
        // public void SessieOpenzetten_net1uurVoorStart_gelukt()  
        public void SessieOpenzetten_MinutenVoorStart_gelukt(int i)
        {
            var a = TestCase.DataTest[i];
            DateTime dt = (DateTime)a[0];
            int min = (int) a[1];
            // Sessie sessie = _sessieLijst.Find(s => s.Equals("sessie1"));
            hedenSessie = _context.hedenSessie;
            Assert.Equal(dt, (hedenSessie.StartDatum.AddMinutes(- min)));
            _alleSessiesLijst.Add(hedenSessie); // sessie nog toevoegen aan lijst
            _hoofdverantwoordelijke.SessieOpenZetten(hedenSessie.SessieID);
          //  Assert.True(_hoofdverantwoordelijke.SessieOpenZetten(sessie.SessieID));
            Assert.Equal(StatusSessie.Open, hedenSessie.StatusSessie);

          /*  Sessie sessie = _sessieLijst.Find(s => s.SessieID.Equals(1));
            DateTime openZettenUur = (new DateTime(2020, 3, 14, 6, 30, 00));
            Assert.Equal(openZettenUur, (sessie.StartDatum.AddMinutes(-60)));
            _alleSessiesLijst.Add(sessie); // sessie nog toevoegen aan lijst
            Assert.True(_hoofdverantwoordelijke.SessieOpenZetten(sessie.SessieID));
            Assert.Equal(StatusSessie.Open, sessie.StatusSessie);
*/
        }
        /*[Fact]
        public void SessieOpenzetten_5minVoorStart_gelukt() // NOG AANPASSEN
        {
            Sessie sessie = _sessieLijst.Find(s => s.SessieID.Equals(1));
            DateTime openZettenUur = (new DateTime(2020, 3, 14, 7, 25, 00));
            Assert.Equal(openZettenUur, (sessie.StartDatum.AddMinutes(-5)));
            _alleSessiesLijst.Add(sessie); // sessie nog toevoegen aan lijst
            Assert.True(_hoofdverantwoordelijke.SessieOpenZetten(sessie.SessieID));
            Assert.Equal(StatusSessie.Open, sessie.StatusSessie);

        }
        [Fact]
        public void SessieOpenzetten_50minVoorStart_gelukt() // NOG AANPASSEN
        {
            Sessie sessie = _sessieLijst.Find(s => s.SessieID.Equals(1));
            DateTime openZettenUur = (new DateTime(2020, 3, 14, 6, 40, 00));
            Assert.Equal(openZettenUur, (sessie.StartDatum.AddMinutes(-50)));
            Assert.Equal(openZettenUur, (sessie.StartDatum.AddHours(-1)));
            _alleSessiesLijst.Add(sessie); // sessie toevoegen aan lijst
            Assert.True(_hoofdverantwoordelijke.SessieOpenZetten(sessie.SessieID));
            Assert.Equal(StatusSessie.Open, sessie.StatusSessie);

        }*/

        [Fact ]
        public void SessieOpenzetten_1uur10minVoorStart_melding()
        {
            // Sessie sessie = _sessieLijst.Find(s => s.Equals("sessie2"));
            hedenSessie = _context.hedenSessie;
            DateTime openZettenUur = (new DateTime(2020, 3, 27, 11, 20, 00));
            _alleSessiesLijst.Add(hedenSessie); // toevoegen sessie
            Assert.NotEqual(openZettenUur, (hedenSessie.StartDatum.AddMinutes(-70)));
            // Assert.False(_hoofdverantwoordelijke.SessieOpenZetten(sessie.SessieID));
            //Assert.Throws<ArgumentException>(() => { return "Sessie mag pas open gezet worden 1 uur voor start van sessie"; });
            Assert.Throws<ArgumentException>(()=> _hoofdverantwoordelijke.SessieOpenZetten(hedenSessie.SessieID));
        }

       

    }
}
