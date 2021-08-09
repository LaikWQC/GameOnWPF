using MyGameTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameTest.Services
{
    public class LocationService : IService
    {
        public void Initialize(IEnumerable<Location> locations)
        {
            Locations = locations.ToList().AsReadOnly();
        }

        public IReadOnlyList<Location> Locations { get; private set; }
    }
}
