﻿using dotnet_g36.Models.Domain;
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
        /// <summary>
        /// Geeft alle sessies, ook de sessies van feedbacklist, media en usersessies
        /// </summary>
        /// <returns>IEnumerable van Sessie</returns>
        public IEnumerable<Sessie> GetAll()
        {
            return _sessies.Include(s => s.Verantwoordelijke).Include(s => s.FeedbackList).ThenInclude(f => f.Auteur).Include(s => s.Media).Include(s => s.GebruikerSessies).ThenInclude(f => f.Gebruiker).OrderBy(m => m.StartDatum); //.ToList()
        }

        /// <summary>
        /// Geeft de juiste sessie weer aan de hand van het sessieId
        /// </summary>
        /// <param name="sessieId">sessieId van de sessie</param>
        /// <returns>Sessie Oject</returns>
        public Sessie GetByID(int sessieId)
        {
            return _sessies.Include(s => s.Verantwoordelijke).Include(s => s.FeedbackList).ThenInclude(f => f.Auteur).Include(s => s.Media).Include(s => s.GebruikerSessies).ThenInclude(f => f.Gebruiker).SingleOrDefault(s => s.SessieID == sessieId);
        }

        /// <summary>
        /// Geeft alle de toekomstige sessies, ook die uit feedbacklist, media en usersessie.
        /// </summary>
        /// <returns>IEnumerable van Sessie</returns>
        public ICollection<Sessie> GetToekomstige()
        {
            return _sessies.Include(s => s.Verantwoordelijke).Include(s => s.FeedbackList).ThenInclude(f => f.Auteur).Include(s => s.Media).Include(s => s.GebruikerSessies).ThenInclude(f => f.Gebruiker).Where(s => s.StartDatum >= DateTime.Now).OrderBy(s => s.StartDatum).ToList();
        }

        /// <summary>
        /// Slaat de veranderingen op in de databank
        /// </summary>
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Geeft de sessie van de meegegeven maand
        /// </summary>
        /// <param name="month">nummer van de opgegeven maand</param>
        /// <returns>IEnumerable van Sessie</returns>
        IEnumerable<Sessie> ISessieRepository.GetByMonth(int month)
        {
            return _sessies.Include(s => s.Verantwoordelijke).Include(s => s.FeedbackList).ThenInclude(f => f.Auteur).Include(s => s.Media).Include(s => s.GebruikerSessies).ThenInclude(f => f.Gebruiker).Where(s => s.StartDatum.Month == month).OrderBy(m => m.StartDatum);
        }
        #endregion
    }

}

