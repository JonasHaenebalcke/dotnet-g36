using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36.Models.Domain
{
    public interface ISessieRepository
    {

        IEnumerable<Sessie> GetAll();
        //ISessieRepository GetByID(int sessieID); //delete?
        IEnumerable<Sessie> GetByID(int sessieID);
        IEnumerable<Sessie> GetByMonth(Month month);
        IEnumerable<Sessie> GetToekomstige();
        void SaveChanges();
        //void Add(Sessie sessie);
        //void Delete(string sessie);
    }
}