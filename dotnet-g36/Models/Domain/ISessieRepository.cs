using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36.Models.Domain
{
    public interface ISessieRepository
    {
        /// <summary>
        /// Geeft alles sessies
        /// </summary>
        /// <returns>IEnumerable van Sessie</returns>
        IEnumerable<Sessie> GetAll();

        //ISessieRepository GetByID(int sessieID); //delete?

        /// <summary>
        /// Geeft de juiste sessie weer aan de hand van opgegeven sessieID
        /// </summary>
        /// <param name="sessieID">sessieID van gekozen sessie</param>
        /// <returns>Sessie Object</returns>
        Sessie GetByID(int sessieID);

        /// <summary>
        /// Geeft de sessie van de meegegeven maand
        /// </summary>
        /// <param name="month">nummer van de opgegeve maand</param>
        /// <returns>IEnumerable van Sessie</returns>
        IEnumerable<Sessie> GetByMonth(int month);

        /// <summary>
        /// Geeft alle de toekomstige sessies
        /// </summary>
        /// <returns>IEnumerable van Sessie</returns>
        IEnumerable<Sessie> GetToekomstige();

        /// <summary>
        /// Slaat de veranderingen op in de databank
        /// </summary>
        void SaveChanges();

        //void Add(Sessie sessie);
        //void Delete(string sessie);
    }
}