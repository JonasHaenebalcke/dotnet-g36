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
               //Er is nog geen constructor
            }
        }
    }
}
