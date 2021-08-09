using GalaSoft.MvvmLight;
using MyGameTest.Models;
using System;

namespace MyGameTest.ViewModels
{
    public class HeroViewModel : ViewModelBase, IUnit, IDisposable
    {
        private readonly LocationHeroData _model;

        public HeroViewModel(LocationHeroData model)
        {
            _model = model;
            _model.AmountChanged += OnAmountChanged;
            _model.CurrentHPChanged += OnHpChanged;
            _model.Data.DataChanged += OnHpChanged;
            _model.IncomingDamageChanged += OnDamageChanged;
        }

        public void Dispose()
        {
            _model.AmountChanged -= OnAmountChanged;
            _model.CurrentHPChanged -= OnHpChanged;
            _model.Data.DataChanged -= OnHpChanged;
            _model.IncomingDamageChanged -= OnDamageChanged;
        }

        #region events
        private void OnAmountChanged()
        {
            RaisePropertyChanged(() => Amount);
        }
        private void OnHpChanged()
        {
            RaisePropertyChanged(() => HpPercentage);
        }
        private void OnDamageChanged()
        {
            RaisePropertyChanged(() => IncomingDamagePerSec);
        }
        #endregion

        public string Name => _model.Data.Name;
        public int Amount
        {
            get => _model.Amount;
            set => _model.Amount = value;
        }
        public double HpPercentage => _model.CurrentHP / _model.Data.HP;
        public string IncomingDamagePerSec => (-_model.IncomingDamagePerSec).ToString("0.##");
    }
}
