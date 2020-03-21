using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.Domain
{
    public interface IGebruikerRepository
    {
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
        /// Geeft de juiste deelnemer/gebruiker aan de hand van barcode
        /// </summary>
        /// <param name="barcode">barcode van de gebruiker</param>
        /// <returns>Gebruiker object</returns>
        Gebruiker GetDeelnemerByBarcode(String barcode);

        ///// <summary>
        ///// Geeft de juiste deelnemer/gebruiker aan de hand van een emailadres
        ///// </summary>
        ///// <param name="emailadres">emailadres van de gebruiker</param>
        ///// <returns>Gebruiker Object</returns>
        //Gebruiker GetDeelnemerByEmail(string emailadres);

        /// <summary>
        /// Geeft de juiste deelnemer/gebruiker aan de hand van een username
        /// </summary>
        /// <param name="username">username van de gebruiker</param>
        /// <returns>Gebruiker Object</returns>
        Gebruiker GetDeelnemerByUsername(string username);

        /// <summary>
        /// Slaat de veranderingen op in de databank
        /// </summary>
        void SaveChanges();
    }
}
