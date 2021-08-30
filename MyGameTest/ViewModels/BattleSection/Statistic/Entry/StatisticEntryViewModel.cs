using GalaSoft.MvvmLight;
using MyGameTest.Models;
using System.Collections.Generic;

namespace MyGameTest.ViewModels
{
    public abstract class StatisticEntryViewModel : ViewModelBase
    {
        protected BattleStatisticEntry _entry;

        public StatisticEntryViewModel()
        {
            Properties.Add(new UnitPropertyViewModel(UnitPropertyType.Damage, "Raw dps from single unit", _getSingleDps));
        }

        public void UpdateEntry(BattleStatisticEntry entry)
        {
            _entry = entry;
            IsActive = entry != null;

            Properties.ForEach(x => x.RaisePropertyChanged());
        }

        public abstract string Name { get; }
        public List<UnitPropertyViewModel> Properties { get; } = new List<UnitPropertyViewModel>();

        private string _getSingleDps() => GetSingleDps.ToString("0.##");
        protected abstract double GetSingleDps { get; }

        public double MyPreference
        {
            get => _myPreference;
            set => Set(() => MyPreference, ref _myPreference, value);
        }
        private double _myPreference;

        public double TotalDps
        {
            get => _totalDps;
            set => Set(() => TotalDps, ref _totalDps, value);
        }
        private double _totalDps;

        public double IncomingSingleDps
        {
            get => _incomingSingleDps;
            set => Set(() => IncomingSingleDps, ref _incomingSingleDps, value);
        }
        private double _incomingSingleDps;

        public double TargetPreference
        {
            get => _targetPreference;
            set => Set(() => TargetPreference, ref _targetPreference, value);
        }
        private double _targetPreference;

        public double IncomingTotalDps
        {
            get => _incomingTotalDps;
            set => Set(() => IncomingTotalDps, ref _incomingTotalDps, value);
        }
        private double _incomingTotalDps;

        public bool IsActive
        {
            get => _isActive;
            set => Set(() => IsActive, ref _isActive, value);
        }
        private bool _isActive;
    }
}
