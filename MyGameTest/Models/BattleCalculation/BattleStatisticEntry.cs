namespace MyGameTest.Models
{
    public class BattleStatisticEntry
    {
        public BattleStatisticEntry(LocationHeroData hero, EnemyData enemy)
        {
            Hero = hero;
            Enemy = enemy;
        }

        public void CalculateHeroSingleDps()
        {
            HeroSingleDps = (Hero.Data.Damage - Enemy.Armor) / Hero.Data.AttackDelay;
            if (HeroSingleDps < 0) HeroSingleDps = 0;
        }
        public void CalculateEnemySingleDps()
        {
            EnemySingleDps = (Enemy.Damage - Hero.Data.Armor) / Enemy.AttackDelay;
            if (EnemySingleDps < 0) EnemySingleDps = 0;
        }
        public void CalculateEnemyWantedLevel()
        {
            EnemyWantedLevel = Enemy.Amount * Enemy.WantedLevel;
        }
        public void CalculateHeroRiskLevel()
        {
            HeroRiskLevel = HeroTotalDps * Hero.Data.AggroLevel;
        }
        public void CalculateHeroTotalDps()
        {
            HeroTotalDps = HeroSingleDps * HeroPreference * Hero.Amount;
        }
        public void CalculateEnemyTotalDps()
        {
            EnemyTotalDps = EnemySingleDps * EnemyPreference * Enemy.Amount;
        }

        public LocationHeroData Hero { get; }
        public EnemyData Enemy { get; }

        public double HeroSingleDps { get; set; }
        public double EnemyWantedLevel { get; set; }
        public double HeroPreference { get; set; }
        public double HeroTotalDps { get; set; }

        public double EnemySingleDps { get; set; }
        public double HeroRiskLevel { get; set; }
        public double EnemyPreference { get; set; }
        public double EnemyTotalDps { get; set; }
    }
}
