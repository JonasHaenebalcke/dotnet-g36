using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36
{
    public interface ISessieRepository
    {

        IEnumerable<Sessie> GetAll();
        ISessieRepository GetByID(int sessieID);
        IEnumerable<Sessie> GetByMonth(Month month);
        void GetToekomstige();
        void SaveChanges();
        //void Add(Sessie sessie);
        //void Delete(string sessie);
    }
}