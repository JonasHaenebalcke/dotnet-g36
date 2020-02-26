using dotnet_g36.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;


namespace dotnet_g36
{
    public class User
    {
        private string _familieNaam;
        private string _voorNaam;

        public String Voornaam
        {
            get => default;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("must have a name");
                _voorNaam = value;
            }
        }

        public String Familienaam
        {
            get => default;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("must have a name");
                _familieNaam = value;
            }
        }

        public List<UserSessie> IngeschrevenSessies
        {
            get => default;
            set
            {
            }
        }

        public int UserID
        {
            get => default;
            set
            {
            }
        }

        public String GebruikersNaam
        {
            get => default;
            set
            {
            }
        }

        public String Wachtwoord
        {
            get => default;
            set
            {
            }
        }

        public StatusGebruiker StatusGebruiker
        {
            get => default;
            set
            {
            }
        }

        public List<UserSessie> AanwezigeSessies
        {
            get => default;
            set
            {
            }
        }

    }
}