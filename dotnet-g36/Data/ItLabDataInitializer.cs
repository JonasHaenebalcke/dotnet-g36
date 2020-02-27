using dotnet_g36.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Data
{
    public class ItLabDataInitializer
    {
        private readonly ApplicationDbContext _context;

        public ItLabDataInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void initializeData() {
            _context.Database.EnsureDeleted();
           
            //Als er nog geen sessie bestaan
            if (_context.Database.EnsureCreated())
            {
                //is shitty niet gebruiken
             //   Sessie sessie = new Sessie(1, new Verantwoordelijke(), new Hoofdverantwoordelijke(), "titel", "gastspreker", "lokaal", new DateTime(), new DateTime(), new DateTime(), new DateTime(), 10, new User(), "beschrijving", new Media(), Month.April, new Feedback(), new User(),)
            }
        }
    }
}