using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Domain
{
    public interface IUserRepository
    {
        IEnumerable<Gebruiker> GetAll();
        IEnumerable<Verantwoordelijke> GetVerantwoordelijken();
        Verantwoordelijke GetVerantwoordelijke(Guid id);
        Verantwoordelijke GetHoofdverantwoordelijke();
        Verantwoordelijke GetVerantwoordelijkeByUsername(string username);
        Gebruiker GetDeelnemerByID(Guid userID);
        Gebruiker GetDeelnemerByEmail(string emailadres);
        Gebruiker GetDeelnemerByUsername(string username);
        IEnumerable<Gebruiker> GetDeelnemers();
        void SaveChanges();
       //void Add(User deelnemer);
       //void Delete(User deelnemer);
    }
}
