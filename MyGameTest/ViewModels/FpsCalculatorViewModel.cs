using GalaSoft.MvvmLight;
using MyGameTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyGameTest.ViewModels
{
    public class FpsCalculatorViewModel : ViewModelBase
    {
        public static FpsCalculatorViewModel Instance { get; private set; }

        public static void Initialize(double updateFrequency = 1)
        {
            Instance = new FpsCalculatorViewModel(updateFrequency);
        }

        private FpsCalculatorViewModel(double updateFrequency) 
        {
            FpsCalculator.Initialize(updateFrequency);
            FpsCalculator.Instance.FpsChanged += OnFpsChanged;
        }

        private void OnFpsChanged()
        {
            RaisePropertyChanged(() => FPS);
        }

        public int FPS => FpsCalculator.Instance.FPS;
    }
}
