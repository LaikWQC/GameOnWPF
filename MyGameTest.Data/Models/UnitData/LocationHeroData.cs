using System;

namespace MyGameTest.Models
{
    public class LocationHeroData
    {
        public LocationHeroData(HeroData data)
        {
            Data = data;

            CurrentHpPercentage = 1;
            DiedInSecond = 0;
        }

        public HeroData Data { get; }

        public void SetIncomingDamage(double damage)
        {
            DiedInSecond = damage / Data.HP;
        }

        public void TakeDamage(double deltaTime)
        {
            var died = DiedInSecond * deltaTime;
            if (CurrentHpPercentage > died)
                CurrentHpPercentage -= died;
            else
            {
                var extraDam = died - CurrentHpPercentage; //TODO учесть что могут убить больше 1го
                CurrentHpPercentage = 1 - extraDam; 
                Die();
            }
        }

        private void Die(int amount = 1)
        {
            var realyDied = Math.Min(Amount, amount);
            Amount -= realyDied;
            if (Amount == 0) 
            {
                CurrentHpPercentage = 1;
                DiedInSecond = 0;
            }
        }

        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount == value) return;
                _amount = value;
                AmountChanged?.Invoke();
            }
        }
        private int _amount;

        public double CurrentHpPercentage
        {
            get => _currentHpPercentage;
            set
            {
                if (_currentHpPercentage == value) return;
                _currentHpPercentage = value;
                CurrentHPChanged?.Invoke();
            }
        }
        private double _currentHpPercentage;

        public double DiedInSecond 
        {
            get => _diedInSecond;
            private set
            {
                if (_diedInSecond == value) return;
                _diedInSecond = value;
                DiedInSecondChanged?.Invoke();
            }
        }
        private double _diedInSecond;

        public event Action AmountChanged;
        public event Action CurrentHPChanged;
        public event Action DiedInSecondChanged;
    }
}
