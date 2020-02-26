using System;
namespace dotnet_g36.Models.Domain
{
    public class Gebruiker : User
    {
        public String Status { get; set; }

        public string Naam
        {
            get
            {
                return _naam;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("must have a name");
                _naam = value;
            }
        }

        public Gebruiker(String naam)
        {
            Naam = naam;
        }
    }
}

