using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Domain
{
    public interface IUserRepository
    {
        /// <summary>
        /// Geefft alle gebruikers
        /// </summary>
        /// <returns>IEnumerable van Gebruiker</returns>
        IEnumerable<Gebruiker> GetAll();

        /// <summary>
        /// Geeft alle verantwoordelijken
        /// </summary>
        /// <returns>IEnumerable van Verantwoordelijke</returns>
        IEnumerable<Verantwoordelijke> GetVerantwoordelijken();

        /// <summary>
        /// Geeft de verantwoordelijke met juiste id
        /// </summary>
        /// <param name="id">idnummer van verantwoordelijke</param>
        /// <returns>Verantwoordelijke Object</returns>
        Verantwoordelijke GetVerantwoordelijke(Guid id);

        /// <summary>
        /// Geeft de hoofdverantwoordelijke
        /// </summary>
        /// <returns>Verantwoordelijke Object</returns>
        Verantwoordelijke GetHoofdverantwoordelijke();

        /// <summary>
        /// Geeft de verantwoordelijke met de juiste username
        /// </summary>
        /// <param name="username">username van verantwoordelijke</param>
        /// <returns>Verantwoordelijke Object</returns>
        Verantwoordelijke GetVerantwoordelijkeByUsername(string username);

        /// <summary>
        /// Geeft de juiste deelnemer/gebruiker aan de hand van userId
        /// </summary>
        /// <param name="userID">userID van de gebruiker</param>
        /// <returns>Gebruiker object</returns>
        Gebruiker GetDeelnemerByID(Guid userID);

        /// <summary>
        /// Geeft de juiste deelnemer/gebruiker aan de hand van barcode
        /// </summary>
        /// <param name="barcode">barcode van de gebruiker</param>
        /// <returns>Gebruiker object</returns>
        Gebruiker GetDeelnemerByBarcode(String barcode);

        /// <summary>
        /// Geeft de juiste deelnemer/gebruiker aan de hand van een emailadres
        /// </summary>
        /// <param name="emailadres">emailadres van de gebruiker</param>
        /// <returns>Gebruiker Object</returns>
        Gebruiker GetDeelnemerByEmail(string emailadres);

        /// <summary>
        /// Geeft de juiste deelnemer/gebruiker aan de hand van een username
        /// </summary>
        /// <param name="username">username van de gebruiker</param>
        /// <returns>Gebruiker Object</returns>
        Gebruiker GetDeelnemerByUsername(string username);

        /// <summary>
        /// Geeft alle deelnemers/gebruikers
        /// </summary>
        /// <returns>IEnumerable van Gebruiker</returns>
        IEnumerable<Gebruiker> GetDeelnemers();

        /// <summary>
        /// Slaat de veranderingen op in de databank
        /// </summary>
        void SaveChanges();

       //void Add(User deelnemer);
       //void Delete(User deelnemer);
    }
}
