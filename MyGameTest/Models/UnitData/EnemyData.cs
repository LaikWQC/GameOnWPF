using MyGameTest.Data;

namespace MyGameTest.Models
{
    public class EnemyData : UnitData
    {
        public EnemyData(EnemyDataDto dto) : base(dto)
        {
            Amount = dto.Amount;
            WantedLevel = dto.WantedLevel;

            CurrentHP = HP;
            IncomingDamagePerSec = 0;
        }

        public int Amount { get; }
        public double WantedLevel { get; }
        public double CurrentHP { get; set; }
        public double IncomingDamagePerSec { get; set; }
    }
}
