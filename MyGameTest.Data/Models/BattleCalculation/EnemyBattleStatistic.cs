using System.Collections.Generic;
using System.Linq;

namespace MyGameTest.Models
{
    public class EnemyBattleStatistic
    {
        public EnemyBattleStatistic(EnemyData enemy, IEnumerable<BattleStatisticEntry> entries)
        {
            Enemy = enemy;
            Entries = entries.ToList();
        }

        public void CalculateTotalDps()
        {
            var totalRisk = Entries.Sum(x => x.HeroRiskLevel);
            foreach (var entry in Entries)
            {
                entry.EnemyPreference = entry.HeroRiskLevel / totalRisk;
                entry.CalculateEnemyTotalDps();
            }
            TotalDps = Entries.Sum(x => x.EnemyTotalDps);
        }

        public void CalculateIncomingDamage()
        {
            TotalIncomingDamage = Entries.Sum(x => x.HeroTotalDps);
            Enemy.SetIncomingDamage(TotalIncomingDamage);
        }

        public EnemyData Enemy { get; }
        public List<BattleStatisticEntry> Entries { get; }
        public double TotalDps { get; private set; }
        public double TotalIncomingDamage { get; private set; }
    }
}
