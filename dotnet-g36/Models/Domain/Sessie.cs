using dotnet_g36.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36
{
    public class Sessie
    {

        public Sessie() { }
        public Sessie(int sessieID, Verantwoordelijke verantwoordelijke, Hoofdverantwoordelijke hoofdverantwoordelijke, string titel, string gastspreker, string lokaal, DateTime startDatum, DateTime eindDatum, DateTime startUur, DateTime eindUur, int aantalOpenPlaatsen, List<User> ingeschrevenen, string beschrijving, List<Media> media, Month month, List<Feedback> feedback, List<User> aanwezigen, List<UserSessie> userSessie)
        {
            SessieID = sessieID;
            Verantwoordelijke = verantwoordelijke;
            Hoofdverantwoordelijke = hoofdverantwoordelijke;
            Titel = titel;
            Gastspreker = gastspreker;
            Lokaal = lokaal;
            StartDatum = startDatum;
            EindDatum = eindDatum;
            StartUur = startUur;
            EindUur = eindUur;
            AantalOpenPlaatsen = aantalOpenPlaatsen;
            Ingeschrevenen = ingeschrevenen;
            Beschrijving = beschrijving;
            Media = media;
            Month = month;
            Feedback = feedback;
            Aanwezigen = aanwezigen;
            UserSessie = userSessie;
        }

        public int SessieID
        {
            get => default;
            set
            {
            }
        }

        public Verantwoordelijke Verantwoordelijke
        {
            get => default;
            set
            {
            }
        }

        public Hoofdverantwoordelijke Hoofdverantwoordelijke
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

        public List<User> Ingeschrevenen
        {
            get => default;
            set
            {
            }
        }

        public String Beschrijving
        {
            get => default;
            set
            {
            }
        }

        public List<Media> Media
        {
            get => default;
            set
            {
            }
        }

        public Month Month
        {
            get => default;
            set
            {
            }
        }

        public List<Feedback> Feedback
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

        public List<UserSessie> UserSessie
        {
            get => default;
            set
            {
            }
        }

        public bool MeldAanwezig(int sessieID)
        {
            throw new System.NotImplementedException();
        }

        public bool SchrijfIn(int sessieID)
        {
            throw new System.NotImplementedException();
        }

        public void OpenZetten()
        {
            throw new System.NotImplementedException();
        }

        public bool SchrijfUit(int sessieID)
        {
            throw new System.NotImplementedException();
        }

        public void FeedbackGeven()
        {
            throw new System.NotImplementedException();
        }
    }
}