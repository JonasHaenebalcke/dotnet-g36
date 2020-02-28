using dotnet_g36.Models.Domain;
using dotnet_g36.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Repositories
{
    public class SessieRepository : ISessieRepository
    {
        #region fields
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Sessie> _sessies;
        #endregion

        #region constructor
        public SessieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _sessies = dbContext.Sessies;
        }
        #endregion

        #region methods
        public void Add(Sessie sessie)
        {
            _sessies.Add(sessie);
        }

        public void Delete(Sessie sessie)
        {
            _sessies.Remove(sessie);
        }

        public IEnumerable<Sessie> GetAll()
        {
            return _sessies;
        }

        //public ISessieRepository GetByID(int sessieId) //delete?
        public IEnumerable<Sessie> GetByID(int sessieId)
        {
            //throw new NotImplementedException();
            return _sessies.Where(s => s.SessieID == sessieId).ToList(); ;
        }

        public IEnumerable<Sessie> GetToekomstige()
        {
            //throw new NotImplementedException();
            return _sessies.Where(s => s.StartDatum >= DateTime.Now).ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        IEnumerable<Sessie> ISessieRepository.GetByMonth(Month month)
        {
            //return _sessies.Where(s => (Month)Enum.Parse(typeof(Month), s.StartDatum.Month.ToString()) == month).ToList(); // throws argumentnullexception //delete?
            //  return _sessies.Where(s => s.StartDatum.Month == (int)month).ToList(); // throws argumentnullexception //beter?
            Hoofdverantwoordelijke admin = new Hoofdverantwoordelijke("Admin", "De Padwin", 0, StatusGebruiker.Actief);
            Verantwoordelijke organizer = new Verantwoordelijke("Organiser", "De SubAdmin", 1, StatusGebruiker.Actief);

            if (month == Month.Februari)
            {
                return new List<Sessie>() { new Sessie(2, admin, organizer, "Sessie Netflix", "BCON",
                new DateTime(2019, 12, 27, 12, 30, 0), new DateTime(2019, 12, 3, 13, 30, 0),
                150, StatusSessie.NietOpen, "Een lezing over Netflix, door een Netflix expert: Jonas Haenebalcke", "Jonas Haenebalcke")
            };
            }
            else if (month == Month.December)
            {
                return new List<Sessie>() { new Sessie(1, admin, organizer, "Sessie 3D Printing", "B1.027",
                new DateTime(2019, 12, 24, 7, 30, 0), new DateTime(2019, 12, 24, 9, 30, 0),
                25, StatusSessie.NietOpen, "Een sessie 3D printing met als gastspreker de geweldige leerkracht Stefaan De Cock",  "Stefaan De Cock")
                };
            }
            else
                throw new GeenSessiesException("Er zijn geen sessies.");
        }
        #endregion
    }

}

