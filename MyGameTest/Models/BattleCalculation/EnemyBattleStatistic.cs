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

        public void Calculate2ndPhase()
        {
            var totalRisk = Entries.Sum(x => x.HeroRiskLevel);
            foreach (var entry in Entries)
            {
                entry.EnemyPreference = entry.HeroRiskLevel / totalRisk;
                entry.CalculateEnemyTotalDps();
            }
        }

        public void CalculateIncomingDamage()
        {
            Enemy.IncomingDamagePerSec = Entries.Sum(x => x.HeroTotalDps);
        }

        public EnemyData Enemy { get; }
        public List<BattleStatisticEntry> Entries { get; }
    }
}
