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
            Properties.Add(new UnitPropertyViewModel(UnitPropertyType.Armor, "Armor", _getArmor));
            Properties.Add(new UnitPropertyViewModel(UnitPropertyType.Attack, "Attack damage", _getAttack));
            Properties.Add(new UnitPropertyViewModel(UnitPropertyType.AtkDelay, "Attack delay", _getAtkDelay));
        }

        public void Dispose()
        {
            _location.LevelChanged -= OnLocationLevelChanged;
            _location.CalculationChanged -= OnCalculationChanged;
        }

        protected virtual void OnLocationLevelChanged() { }
        protected virtual void OnCalculationChanged() { }

        #region Properties
        private string _getHP() => $"{HP}";
        protected abstract int HP { get; }

        private string _getArmor() => $"{Armor}";
        protected abstract int Armor { get; }

        private string _getAttack() => $"{Attack}";
        protected abstract int Attack { get; }

        private string _getAtkDelay() => $"{AtkDelay.ToString("0.##")}s";
        protected abstract double AtkDelay { get; }
        #endregion

        public abstract string Name { get; }
        public List<UnitPropertyViewModel> Properties { get; } = new List<UnitPropertyViewModel>();
        public ObservableCollection<StatisticEntryViewModel> Entries { get; } = new ObservableCollection<StatisticEntryViewModel>(); 
    }
}
