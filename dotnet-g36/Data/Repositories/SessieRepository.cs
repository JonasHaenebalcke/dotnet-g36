using dotnet_g36.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return _sessies.Include(s => s.UserSessies);
        }

        //public ISessieRepository GetByID(int sessieId) //delete?
        public Sessie GetByID(int sessieId)
        {
            //throw new NotImplementedException();
            return _sessies.Include(s => s.UserSessies).SingleOrDefault(s => s.SessieID == sessieId);
        }

        public IEnumerable<Sessie> GetToekomstige()
        {
            //throw new NotImplementedException();
            return _sessies.Include(s => s.UserSessies).Where(s => s.StartDatum >= DateTime.Now).ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        IEnumerable<Sessie> ISessieRepository.GetByMonth(int month)
        {
            return _sessies.Include(s => s.UserSessies).Where(s => s.StartDatum.Month == month).OrderBy(m => m.StartDatum);
        }
        #endregion
    }

}

