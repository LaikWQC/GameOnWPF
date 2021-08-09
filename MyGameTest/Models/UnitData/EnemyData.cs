namespace MyGameTest.Models
{
    public class EnemyData : UnitData
    {
        public EnemyData()
        {
            CurrentHP = HP;
        }

        public int Amount { get; }
        public double WantedLevel { get; }
        public double CurrentHP { get; set; }
        public double IncomingDamagePerSec { get; set; } = 0;
    }
}
