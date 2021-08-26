using MyGameTest.Data;
using System;

namespace MyGameTest.Models
{
    public class EnemyData : UnitData
    {
        public EnemyData(EnemyDataDto dto) : base(dto)
        {
            Amount = dto.Amount;
            WantedLevel = dto.WantedLevel;

            CurrentHpPercentage = 1;
            DiedInSecond = 0;
        }

        public void SetIncomingDamage(double damage)
        {
            DiedInSecond = damage / HP;
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
            //TODO начислить награду
        }

        public int Amount { get; }
        public double WantedLevel { get; }
        public double CurrentHpPercentage
        {
            get => _currentHpPercentage;
            private set
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

        public event Action CurrentHPChanged;
        public event Action DiedInSecondChanged;
    }
}
