using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Domain
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        IEnumerable<Verantwoordelijke> GetVerantwoordelijken();
        Verantwoordelijke GetHoofdverantwoordelijke();
        User GetDeelnemerByID(int userID);
        void SaveChanges();
       //void Add(User deelnemer);
       //void Delete(User deelnemer);
    }
}
