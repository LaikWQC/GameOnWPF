using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyGameTest.Models;
using System;
using System.Windows.Input;

namespace MyGameTest.ViewModels
{
    public class HeroViewModel : ViewModelBase, IDisposable
    {
        public LocationHeroData Model { get; }

        public HeroViewModel(LocationHeroData model)
        {
            Model = model;
            Model.AmountChanged += OnAmountChanged;
            Model.CurrentHPChanged += OnHpChanged;
            Model.DiedInSecondChanged += OnDiedInSecondChanged;

            CmdPlusAmount = new RelayCommand(PlusAmount);
            CmdMinusAmount = new RelayCommand(MinusAmount);
        }

        public void Dispose()
        {
            Model.AmountChanged -= OnAmountChanged;
            Model.CurrentHPChanged -= OnHpChanged;
            Model.DiedInSecondChanged -= OnDiedInSecondChanged;
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

        public string Name => Model.Data.Name;
        public int Amount => Model.Amount;
        public double CurrentHpPercentage => Model.CurrentHpPercentage;
        public string DiedInSecond => (-Model.DiedInSecond).ToString("0.##");

        public void PlusAmount()
        {
            Model.Amount++;
        }
        public void MinusAmount()
        {
            Model.Amount--;
        }

        public ICommand CmdPlusAmount { get; }
        public ICommand CmdMinusAmount { get; }
    }
}
