using System.Collections.Generic;
using System.Linq;

namespace MyGameTest.Models
{
    public class HeroBattleStatistic
    {
        public HeroBattleStatistic(LocationHeroData hero, IEnumerable<BattleStatisticEntry> entries)
        {
            Hero = hero;
            Entries = entries.ToList();
        }
        
        public void Calculate1stPhase()
        {
            foreach(var entry in Entries)
            {
                entry.CalculateHeroSingleDps();
                entry.CalculateEnemySingleDps();
                entry.CalculateEnemyWantedLevel();
            }
            var totalWanted = Entries.Sum(x => x.EnemyWantedLevel);
            foreach(var entry in Entries)
            {
                entry.HeroPreference = entry.EnemyWantedLevel / totalWanted;
                entry.CalculateHeroTotalDps();
            }
        }

        public void CalculateIncomingDamage()
        {
            Hero.IncomingDamagePerSec = Entries.Sum(x => x.EnemyTotalDps);
        }

        public LocationHeroData Hero { get; }
        public List<BattleStatisticEntry> Entries { get; }
    }
}
