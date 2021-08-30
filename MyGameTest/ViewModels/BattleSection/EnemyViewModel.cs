using GalaSoft.MvvmLight;
using MyGameTest.Models;
using System;

namespace MyGameTest.ViewModels
{
    public class EnemyViewModel : ViewModelBase, IDisposable
    {
        private readonly EnemyData _model;

        public EnemyViewModel(EnemyData model)
        {
            _model = model;
            _model.CurrentHPChanged += OnHpChanged;
            _model.DiedInSecondChanged += OnDiedInSecondChanged;
        }

        public void Dispose()
        {
            _model.CurrentHPChanged -= OnHpChanged;
            _model.DiedInSecondChanged -= OnDiedInSecondChanged;
        }

        #region events
        private void OnHpChanged()
        {
            RaisePropertyChanged(() => CurrentHpPercentage);
        }
        private void OnDiedInSecondChanged()
        {
            RaisePropertyChanged(() => DiedInSecond);
        }
        #endregion

        public string Name => _model.Name;
        public int Amount => _model.Amount;
        public double CurrentHpPercentage => _model.CurrentHpPercentage;
        public string DiedInSecond => (-_model.DiedInSecond).ToString("0.##");
    }
}
