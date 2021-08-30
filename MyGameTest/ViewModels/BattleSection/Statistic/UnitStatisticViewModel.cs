using MyGameTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyGameTest.ViewModels
{
    public abstract class UnitStatisticViewModel : IDisposable
    {
        protected readonly Location _location;

        public UnitStatisticViewModel(Location location)
        {
            _location = location;
            _location.LevelChanged += OnLocationLevelChanged;
            _location.CalculationChanged += OnCalculationChanged;

            Properties.Add(new UnitPropertyViewModel(UnitPropertyType.HP, "Hit points", _getHP));
        }

        public void Dispose()
        {
            _location.LevelChanged -= OnLocationLevelChanged;
            _location.CalculationChanged -= OnCalculationChanged;
        }

        protected virtual void OnLocationLevelChanged() { }
        protected virtual void OnCalculationChanged() { }

        protected abstract string _getHP();

        public abstract string Name { get; }
        public List<UnitPropertyViewModel> Properties { get; } = new List<UnitPropertyViewModel>();
        public ObservableCollection<StatisticEntryViewModel> Entries { get; } = new ObservableCollection<StatisticEntryViewModel>(); 
    }
}
