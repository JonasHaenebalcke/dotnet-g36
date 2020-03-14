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
        public void Add(Gebruiker deelnemer)
        {
            if (!_users.Contains(deelnemer))
                _users.Add(deelnemer);
        }

        public void Delete(Gebruiker deelnemer)
        {
            _users.Remove(deelnemer);
        }
        public IEnumerable<Gebruiker> GetAll()
        {
            return _users.Include(s => s.UserSessies).ToList();
        }

        public Gebruiker GetDeelnemerByID(Guid userID)
        {
            return _users.Include(s => s.UserSessies).SingleOrDefault(u => u.Id.Equals(userID));
        }

        public Verantwoordelijke GetHoofdverantwoordelijke()
        {
            // er is maar 1 hoofdvernatwoordelijke die in de lijst van verantwoordelijke zit
            return _verantwoordelijken.Include(s => s.UserSessies).SingleOrDefault(h => h.IsHoofdverantwoordelijke == true);
        }
        public Verantwoordelijke GetVerantwoordelijke(int userID)
        {
            return _verantwoordelijken.Include(s => s.UserSessies).SingleOrDefault(v => v.Id.Equals(userID));
        }

        public IEnumerable<Verantwoordelijke> GetVerantwoordelijken()
        {
            return _verantwoordelijken.Include(s => s.UserSessies).ToList();
        }

        public IEnumerable<Gebruiker> GetDeelnemers()
        {
            return _users.Include(s => s.UserSessies).Where(d => d.GetType().Equals("Deelnemer")).ToList();
        }

        public Gebruiker GetDeelnemerByEmail(string emailadres) // Misschien moet er nog een include in...
        {
            return _users.Include(s => s.UserSessies).SingleOrDefault(d => d.Email.Equals(emailadres));
        }

        public Gebruiker GetDeelnemerByUsername(string username)
        {
            return _users.Include(s => s.UserSessies).SingleOrDefault(d => d.UserName.Equals(username));
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }


        #endregion
    }
}
