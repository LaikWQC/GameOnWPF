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
            Enemies = new ObservableCollection<EnemyViewModel>(_model.Enemies.Select(x=> new EnemyViewModel(x)));
        }

        public ObservableCollection<HeroViewModel> Heroes { get; }
        public ObservableCollection<EnemyViewModel> Enemies { get; }

        public HeroViewModel SelectedHero
        {
            get => _selectedHero;
            set
            {
                if (!Set(() => SelectedHero, ref _selectedHero, value)) return;
                if (value != null)
                {
                    Set(() => SelectedEnemy, ref _selectedEnemy, null);
                    UnitStatistic = new HeroStatisticViewModel(value, _model);
                }
                else UnitStatistic = null;
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
                {
                    Set(() => SelectedHero, ref _selectedHero, null);
                }
                else UnitStatistic = null;
            }
        }
        private EnemyViewModel _selectedEnemy;

        public UnitStatisticViewModel UnitStatistic 
        {
            get => _unitStatistic;
            set
            {
                if (_unitStatistic == value) return;
                _unitStatistic?.Dispose();
                _unitStatistic = value;
                RaisePropertyChanged(() => UnitStatistic);
            }
        }
        private UnitStatisticViewModel _unitStatistic;
    }
}
