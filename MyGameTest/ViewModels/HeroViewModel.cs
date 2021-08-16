using GalaSoft.MvvmLight;
using MyGameTest.Models;
using System;

namespace MyGameTest.ViewModels
{
    public class HeroViewModel : ViewModelBase, IDisposable
    {
        private readonly LocationHeroData _model;

        public HeroViewModel(LocationHeroData model)
        {
            _model = model;
            _model.AmountChanged += OnAmountChanged;
            _model.CurrentHPChanged += OnHpChanged;
            _model.DiedInSecondChanged += OnDiedInSecondChanged;
        }

        public void Dispose()
        {
            _model.AmountChanged -= OnAmountChanged;
            _model.CurrentHPChanged -= OnHpChanged;
            _model.DiedInSecondChanged -= OnDiedInSecondChanged;
        }

        #region events
        private void OnAmountChanged()
        {
            RaisePropertyChanged(() => Amount);
        }
        private void OnHpChanged()
        {
            RaisePropertyChanged(() => CurrentHpPercentage);
        }
        private void OnDiedInSecondChanged()
        {
            RaisePropertyChanged(() => DiedInSecond);
        }
        #endregion

        public string Name => _model.Data.Name;
        public int Amount
        {
            get => _model.Amount;
            set => _model.Amount = value;
        }
        public double CurrentHpPercentage => _model.CurrentHpPercentage;
        public string DiedInSecond => (-_model.DiedInSecond).ToString("0.##");
    }
}
