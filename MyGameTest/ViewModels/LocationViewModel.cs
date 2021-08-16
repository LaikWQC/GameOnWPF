using GalaSoft.MvvmLight;
using MyGameTest.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyGameTest.ViewModels
{
    public class LocationViewModel : ViewModelBase
    {
        private readonly Location _model;

        public LocationViewModel(Location model)
        {
            _model = model;

            Heroes = new ObservableCollection<HeroViewModel>(_model.Heroes.Select(x => new HeroViewModel(x)));
            Enemies = new ObservableCollection<EnemyData>(_model.Enemies);
        }

        public ObservableCollection<HeroViewModel> Heroes { get; }
        public ObservableCollection<EnemyData> Enemies { get; }
    }
}
