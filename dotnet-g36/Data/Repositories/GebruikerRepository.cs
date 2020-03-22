using dotnet_g36.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        #region Fields
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Gebruiker> _users;
        private readonly DbSet<Verantwoordelijke> _verantwoordelijken;
        #endregion

        #region Constructors
        public GebruikerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _users = _dbContext.Gebruikers;
            _verantwoordelijken = _dbContext.Verantwoordelijken;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Geeft de juiste gebruiker aan de hand van barcode
        /// </summary>
        /// <param name="barcode">barcode van de gebruiker</param>
        /// <returns>Gebruiker object</returns>
        public Gebruiker GetDeelnemerByBarcode(String barcode)
        {
            return _users.Include(s => s.GebruikerSessies).ThenInclude(usl => usl.Gebruiker).SingleOrDefault(u => u.Barcode.Equals(barcode));
        }

        /// <summary>
        /// Geeft de hoofdverantwoordelijke
        /// </summary>
        /// <returns>Verantwoordelijke Object</returns>
        public Verantwoordelijke GetHoofdverantwoordelijke()
        {
            return _verantwoordelijken.Include(s => s.OpenTeZettenSessies).Include(s => s.GebruikerSessies).ThenInclude(usl => usl.Gebruiker).SingleOrDefault(h => h.IsHoofdverantwoordelijke == true);
        }

        ///// <summary>
        ///// Geeft de juiste deelnemer/gebruiker aan de hand van een emailadres
        ///// </summary>
        ///// <param name="emailadres">emailadres van de gebruiker</param>
        ///// <returns>Gebruiker Object</returns>
        //public Gebruiker GetDeelnemerByEmail(string emailadres)
        //{
        //    return _users.Include(s => s.GebruikerSessies).ThenInclude(usl => usl.Select(us => us.Gebruiker)).SingleOrDefault(d => d.Email.Equals(emailadres));
        //}

        /// <summary>
        /// Geeft de juiste deelnemer/gebruiker aan de hand van een username
        /// </summary>
        /// <param name="username">username van de gebruiker</param>
        /// <returns>Gebruiker Object</returns>
        public Gebruiker GetDeelnemerByUsername(string username)
        {
            return _users.Include(s => s.GebruikerSessies).ThenInclude(usl => usl.Gebruiker)
                .Include(s => s.GebruikerSessies).ThenInclude(usl => usl.Sessie).SingleOrDefault(d => d.UserName.Equals(username));
        }

        /// <summary>
        /// Slaat de veranderingen op in de databank
        /// </summary>
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Geeft de juiste Verantwoordelijke aan de hand van een username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Verantwoordelijke object</returns>
        public Verantwoordelijke GetVerantwoordelijkeByUsername(string username)
        {
            return _verantwoordelijken.Include(s => s.GebruikerSessies).ThenInclude(usl => usl.Gebruiker)
                .Include(s => s.GebruikerSessies).ThenInclude(usl => usl.Sessie).Include(s => s.OpenTeZettenSessies).SingleOrDefault(d => d.UserName.Equals(username));
        }

        #endregion
    }
}
