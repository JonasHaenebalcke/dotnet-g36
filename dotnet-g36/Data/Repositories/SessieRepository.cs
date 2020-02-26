﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Repositories
{
    public class SessieRepository : ISessieRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Sessie> _sessies;

        public SessieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _sessies = dbContext.Sessies;
        }

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

        public ISessieRepository GetByID(int sessieId)
        {
            throw new NotImplementedException();
        }
        public ISessieRepository GetByMonth()
        {
            throw new NotImplementedException();
        }

        public void GetToekomstige()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        IEnumerable<Sessie> ISessieRepository.GetByMonth()
        {
            throw new NotImplementedException();
        }
    }

}

