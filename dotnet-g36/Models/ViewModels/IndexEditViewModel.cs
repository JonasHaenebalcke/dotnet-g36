using dotnet_g36.Data.Repositories;
using dotnet_g36.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Models.ViewModels
{
    public class IndexEditViewModel
    {
        public IEnumerable<Sessie> Sessies { get; set; }

        public IndexEditViewModel() { }

        public IndexEditViewModel(IEnumerable<Sessie> sessies)

        {
            Sessies = sessies;
        }
    }
}
