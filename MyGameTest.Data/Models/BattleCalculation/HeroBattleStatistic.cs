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
        
        public void CalculateTotalDps()
        {
            var totalWanted = Entries.Sum(x => x.EnemyWantedLevel);
            foreach(var entry in Entries)
            {
                entry.HeroPreference = entry.EnemyWantedLevel / totalWanted;
                entry.CalculateHeroTotalDps();
            }
            TotalDps = Entries.Sum(x => x.HeroTotalDps);
        }

        public void CalculateIncomingDamage()
        {
            TotalIncomingDamage = Entries.Sum(x => x.EnemyTotalDps);
            Hero.SetIncomingDamage(TotalIncomingDamage);
        }

        public LocationHeroData Hero { get; }
        public List<BattleStatisticEntry> Entries { get; }
        public double TotalDps { get; private set; }
        public double TotalIncomingDamage { get; private set; }
    }
}
