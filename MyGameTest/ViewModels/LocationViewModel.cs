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

            Heroes = new List<HeroViewModel>(_model.Heroes.Select(x => new HeroViewModel(x)));
            Enemies = new List<EnemyViewModel>(_model.Enemies.Select(x=> new EnemyViewModel(x)));
        }

        public List<HeroViewModel> Heroes { get; }
        public List<EnemyViewModel> Enemies { get; }

        public HeroViewModel SelectedHero
        {
            get => _selectedHero;
            set
            {
                if (!Set(() => SelectedHero, ref _selectedHero, value)) return;
                if (value != null) 
                    Set(() => SelectedEnemy, ref _selectedEnemy, null);
            }
        }
        private HeroViewModel _selectedHero;

        public EnemyViewModel SelectedEnemy
        {
            get => _selectedEnemy;
            set
            {
                if (!Set(() => SelectedEnemy, ref _selectedEnemy, value)) return;
                if (value != null)
                    Set(() => SelectedHero, ref _selectedHero, null);
            }
        }
        private EnemyViewModel _selectedEnemy;
    }
}
