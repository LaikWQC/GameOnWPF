using MyGameTest.Data;

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

        public int Amount { get; }
        public double WantedLevel { get; }
        public double CurrentHpPercentage { get; private set; }
        public double DiedInSecond { get; private set; }
    }
}
