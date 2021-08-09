using System;

namespace MyGameTest.Models
{
    public class LocationHeroData
    {
        public LocationHeroData(HeroData data)
        {
            Data = data;

            CurrentHP = Data.HP;
            IncomingDamagePerSec = 0;
        }

        public HeroData Data { get; }

        public void TakeDamage(double deltaTime)
        {
            var damage = IncomingDamagePerSec * deltaTime;
            if (CurrentHP < damage)
            {
                var extraDam = damage - CurrentHP;
                CurrentHP = Data.HP - extraDam; //TODO типа убьют не больше 1го
                Die();
            }
            else
                CurrentHP -= damage;
        }

        public void Die()
        {
            Amount--;
            if(Amount==0)
            {
                CurrentHP = Data.HP;
                IncomingDamagePerSec = 0;
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

        public double CurrentHP
        {
            get => _currentHP;
            set
            {
                if (_currentHP == value) return;
                _currentHP = value;
                CurrentHPChanged?.Invoke();
            }
        }
        private double _currentHP;

        public double IncomingDamagePerSec 
        { 
            get => _incomingDamagePerSec;
            set
            {
                if (_incomingDamagePerSec == value) return;
                _incomingDamagePerSec = value;
                IncomingDamageChanged?.Invoke();
            }
        }
        private double _incomingDamagePerSec;

        public event Action AmountChanged;
        public event Action CurrentHPChanged;
        public event Action IncomingDamageChanged;
    }
}
