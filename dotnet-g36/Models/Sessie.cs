using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36
{
    public class Sessie
    {
        public Sessie()
        {
            throw new System.NotImplementedException();
        }

        public User aanwezig
        {
            get => default;
            set
            {
            }
        }

        public int SessieID
        {
            get => default;
            set
            {
            }
        }

        public string NaamVerantwoordelijke
        {
            get => default;
            set
            {
            }
        }

        public string Titel
        {
            get => default;
            set
            {
            }
        }

        public string Gastspreker
        {
            get => default;
            set
            {
            }
        }

        public string Lokaal
        {
            get => default;
            set
            {
            }
        }

        public DateTime StartDatum
        {
            get => default;
            set
            {
            }
        }

        public DateTime EindDatum
        {
            get => default;
            set
            {
            }
        }

        public DateTime StartUur
        {
            get => default;
            set
            {
            }
        }

        public DateTime EindUur
        {
            get => default;
            set
            {
            }
        }

        public int AantalOpenPlaatsen
        {
            get => default;
            set
            {
            }
        }

        public bool Aanwezigheid
        {
            get => default;
            set
            {
            }
        }

        public List<User> Aanwezigen
        {
            get => default;
            set
            {
            }
        }

        public List<User> Ingeschrevenen
        {
            get => default;
            set
            {
            }
        }

        public string Beschrijving
        {
            get => default;
            set
            {
            }
        }

        public List<string> Media
        {
            get => default;
            set
            {
            }
        }

        public Months Months
        {
            get => default;
            set
            {
            }
        }

        public List<string> Feedback
        {
            get => default;
            set
            {
            }
        }

        public void MeldAanwezig(int sessieID)
        {
            throw new System.NotImplementedException();
        }

        public void SchrijfIn(int sessieID)
        {
            throw new System.NotImplementedException();
        }

        public void OpenZetten()
        {
            throw new System.NotImplementedException();
        }

        public void SchrijfUit()
        {
            throw new System.NotImplementedException();
        }

        public void FeedbackGeven()
        {
            throw new System.NotImplementedException();
        }
    }
}