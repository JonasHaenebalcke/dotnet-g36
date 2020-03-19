using dotnet_g36.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Fields
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Gebruiker> _users;
        private readonly DbSet<Verantwoordelijke> _verantwoordelijken;
        #endregion

        #region Constructors
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _users = _dbContext.Gebruikers;
            _verantwoordelijken = _dbContext.Verantwoordelijken;
        }
        #endregion

        #region Methods
       /* /// <summary>
        /// Voegd de meegegeven gebruiker toe
        /// </summary>
        /// <param name="deelnemer">Deelnemer Object</param>
        public void Add(Gebruiker deelnemer)
        {
            if (!_users.Contains(deelnemer))
                _users.Add(deelnemer);
        }

        /// <summary>
        /// Verwijderd de meegegeven gebruiker
        /// </summary>
        /// <param name="deelnemer">Gebruiker Object</param>
        public void Delete(Gebruiker deelnemer)
        {
            _users.Remove(deelnemer);
        }*/

        /// <summary>
        /// Geefft alle gebruikers, ook die uit usersessies
        /// </summary>
        /// <returns>IEnumerable van Gebruiker</returns>
        public IEnumerable<Gebruiker> GetAll()
        {
            return _users.Include(s => s.UserSessies).ThenInclude(usl => usl.Select(us => us.User)).ToList();
        }

        /// <summary>
        /// Geeft de juiste deelnemer/gebruiker aan de hand van userId
        /// </summary>
        /// <param name="userID">userID van de gebruiker</param>
        /// <returns>Gebruiker object</returns>
        public Gebruiker GetDeelnemerByID(Guid userID)
        {
            return _users.Include(s => s.UserSessies).ThenInclude(usl => usl.Select(us => us.User)).SingleOrDefault(u => u.Id.Equals(userID));
        }

        public Gebruiker GetDeelnemerByBarcode(String barcode)
        {
            return _users.Include(s => s.UserSessies).ThenInclude(usl => usl.Select(us => us.User)).SingleOrDefault(u => u.Barcode.Equals(barcode));
        }

        /// <summary>
        /// Geeft de hoofdverantwoordelijke
        /// </summary>
        /// <returns>Verantwoordelijke Object</returns>
        public Verantwoordelijke GetHoofdverantwoordelijke()
        {
            return _verantwoordelijken.Include(s => s.OpenTeZettenSessies).Include(s => s.UserSessies).ThenInclude(usl => usl.Select(us => us.User)).SingleOrDefault(h => h.IsHoofdverantwoordelijke == true);
        }

        /// <summary>
        /// Geeft de juiste verantwoordelijke 
        /// </summary>
        /// <param name="userID">userID van de gebruiker</param>
        /// <returns>Verantwoordelijke Object</returns>
        public Verantwoordelijke GetVerantwoordelijke(Guid userID)
        {
            return _verantwoordelijken.Include(s => s.OpenTeZettenSessies).Include(s => s.UserSessies).ThenInclude(usl => usl.Select(us => us.User)).SingleOrDefault(v => v.Id.Equals(userID));
        }

        /// <summary>
        /// Geeft alle verantwoordelijken
        /// </summary>
        /// <returns>IEnumerable van Verantwoordelijke</returns>
        public IEnumerable<Verantwoordelijke> GetVerantwoordelijken()
        {
            return _verantwoordelijken.Include(s => s.OpenTeZettenSessies).Include(s => s.UserSessies).ToList();
        }

        /// <summary>
        /// Geeft alle deelnemers/gebruikers
        /// </summary>
        /// <returns>IEnumerable van Gebruiker</returns>
        public IEnumerable<Gebruiker> GetDeelnemers()
        {
            return _users.Include(s => s.UserSessies).ThenInclude(usl => usl.Select(us => us.User)).Where(d => d.GetType().Equals("Deelnemer")).ToList();
        }

        /// <summary>
        /// Geeft de juiste deelnemer/gebruiker aan de hand van een emailadres
        /// </summary>
        /// <param name="emailadres">emailadres van de gebruiker</param>
        /// <returns>Gebruiker Object</returns>
        public Gebruiker GetDeelnemerByEmail(string emailadres)
        {
            return _users.Include(s => s.UserSessies).ThenInclude(usl => usl.Select(us => us.User)).SingleOrDefault(d => d.Email.Equals(emailadres));
        }

        /// <summary>
        /// Geeft de juiste deelnemer/gebruiker aan de hand van een username
        /// </summary>
        /// <param name="username">username van de gebruiker</param>
        /// <returns>Gebruiker Object</returns>
        public Gebruiker GetDeelnemerByUsername(string username)
        {
            return _users.Include(s => s.UserSessies).ThenInclude(usl => usl.Select(us => us.User)).SingleOrDefault(d => d.UserName.Equals(username));
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
            return _verantwoordelijken.Include(s => s.UserSessies).ThenInclude(usl => usl.Select(us => us.User)).Include(s => s.OpenTeZettenSessies).SingleOrDefault(d => d.UserName.Equals(username));
        }

        #endregion
    }
}
