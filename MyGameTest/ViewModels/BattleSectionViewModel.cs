using MyGameTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameTest.ViewModels
{
    public class BattleSectionViewModel
    {
        private LocationService _locationService;

        public BattleSectionViewModel()
        {
            _locationService = ServiceLocator.Current.GetService<LocationService>();
            CurrentLocation = new LocationViewModel(_locationService.Locations.First());
        }

        public LocationViewModel CurrentLocation { get; set; }
    }
}
