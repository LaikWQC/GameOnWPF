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
            Properties.Add(new UnitPropertyViewModel(UnitPropertyType.Attack, "Raw dps from single unit", _getSingleDps));
            Properties.Add(new UnitPropertyViewModel(UnitPropertyType.Preference, "How likely you'll attack this target", _getMyPreference));
            Properties.Add(new UnitPropertyViewModel(UnitPropertyType.TotalAttack, "Total dps from all units", _getTotalDps));
        }

        public void UpdateEntry(BattleStatisticEntry entry)
        {
            _entry = entry;
            IsActive = entry != null;

            Properties.ForEach(x => x.RaisePropertyChanged());
        }

        public abstract string Name { get; }
        public List<UnitPropertyViewModel> Properties { get; } = new List<UnitPropertyViewModel>();

        #region Properties
        private string _getSingleDps() => SingleDps.ToString("0.#");
        protected abstract double SingleDps { get; }

        private string _getMyPreference() => $"{(MyPreference * 100).ToString("0")}%";
        protected abstract double MyPreference { get; }

        private string _getTotalDps() => TotalDps.ToString("0.#");
        protected abstract double TotalDps { get; }
        #endregion

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
