﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyGameTest.Models;
using System;
using System.Windows.Input;

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

            CmdPlusAmount = new RelayCommand(PlusAmount);
            CmdMinusAmount = new RelayCommand(MinusAmount);
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
        public int Amount => _model.Amount;
        public double CurrentHpPercentage => _model.CurrentHpPercentage;
        public string DiedInSecond => (-_model.DiedInSecond).ToString("0.##");

        public void PlusAmount()
        {
            _model.Amount++;
        }
        public void MinusAmount()
        {
            _model.Amount--;
        }

        public ICommand CmdPlusAmount { get; }
        public ICommand CmdMinusAmount { get; }
    }
}
