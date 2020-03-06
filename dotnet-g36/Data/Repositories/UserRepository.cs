﻿using dotnet_g36.Models.Domain;
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
        private readonly DbSet<User> _users;
        private readonly DbSet<Verantwoordelijke> _verantwoordelijken;
        #endregion

        #region Constructors
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _users = _dbContext.Deelnemers;
            _verantwoordelijken = _dbContext.Verantwoordelijken;
        }
        #endregion

        #region Methods
        public void Add(User deelnemer)
        {
            if (!_users.Contains(deelnemer))
                _users.Add(deelnemer);
        }

        public void Delete(User deelnemer)
        {
            _users.Remove(deelnemer);
        }
        public IEnumerable<User> GetAll()
        {
            return _users.ToList();
        }

        public User GetDeelnemerByID(int userID)
        {
            return _users.SingleOrDefault(u => u.UserID.Equals(userID));
        }

        public Verantwoordelijke GetHoofdverantwoordelijke()
        {
            // er is maar 1 hoofdvernatwoordelijke die in de lijst van verantwoordelijke zit
            return _verantwoordelijken.SingleOrDefault(h => h.IsHoofdverantwoordelijke == true);
        }
        public Verantwoordelijke GetVerantwoordelijke(int userID)
        {
            return _verantwoordelijken.SingleOrDefault(v => v.UserID.Equals(userID));
        }

        public IEnumerable<Verantwoordelijke> GetVerantwoordelijken()
        {
            return _verantwoordelijken.ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        } 
        #endregion
    }
}