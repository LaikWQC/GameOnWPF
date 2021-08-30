using GalaSoft.MvvmLight;
using System;

namespace MyGameTest.ViewModels
{
    public class UnitPropertyViewModel : ViewModelBase
    {
        private Func<string> _getValue;

        public UnitPropertyViewModel(UnitPropertyType type, string tooltip, Func<string> valueGetter)
        {
            Type = type;
            Tooltip = tooltip;
            _getValue = valueGetter;
        }

        public UnitPropertyType Type { get; }
        public string Tooltip { get; }
        public string Value => _getValue();

        public void RaisePropertyChanged()
        {
            RaisePropertyChanged(() => Value);
        }
    }
}
