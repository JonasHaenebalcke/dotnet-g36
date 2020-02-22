using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36
{
    public interface ISessieRepository
    {
        Sessie Sessie { get; set; }

        System.Collections.Generic.IEnumerable<dotnet_g36.Sessie> GetAll();
        IsessieRepository GetByID(int sessieID);
        IEnumerable<Sessie> GetByMonth();
        void GetToekomstige();
        void SaveChanges();
        void Add(Sessie sessie);
        void Delete(string sessie);
    }
}