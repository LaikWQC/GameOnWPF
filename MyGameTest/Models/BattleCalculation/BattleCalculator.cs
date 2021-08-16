using System.Collections.Generic;
using System.Linq;

namespace MyGameTest.Models
{
    public class BattleCalculator
    {
        private List<LocationHeroData> _heroes;
        private List<EnemyData> _enemies;
        private Dictionary<LocationHeroData, HeroBattleStatistic> _heroStatistics = new Dictionary<LocationHeroData, HeroBattleStatistic>();
        private Dictionary<EnemyData, EnemyBattleStatistic> _enemyStatistics = new Dictionary<EnemyData, EnemyBattleStatistic>();

        public BattleCalculator(IEnumerable<LocationHeroData> heroes, IEnumerable<EnemyData> enemies)
        {
            _heroes = heroes.ToList();
            _enemies = enemies.ToList();
        }

        public void Calculate()
        {
            _heroStatistics.Clear();
            _enemyStatistics.Clear();

            //если героев нет, то и рассчитывать нечего
            if (_heroes.All(x => x.Amount == 0))
            {
                _heroes.ForEach(x => x.SetIncomingDamage(0));
                _enemies.ForEach(x => x.SetIncomingDamage(0));
                return;
            }

            //создание статистики для участвующих героев. Остальных героев никто не бьет 
            var statisticEntries = new List<BattleStatisticEntry>();
            foreach (var hero in _heroes)
            {
                if (hero.Amount > 0)
                    foreach (var enemy in _enemies)
                        statisticEntries.Add(new BattleStatisticEntry(hero, enemy));
                else
                    hero.SetIncomingDamage(0);
            }
            foreach (var group in statisticEntries.GroupBy(x => x.Hero))
                _heroStatistics[group.Key] = new HeroBattleStatistic(group.Key, group);
            foreach (var group in statisticEntries.GroupBy(x => x.Enemy))
                _enemyStatistics[group.Key] = new EnemyBattleStatistic(group.Key, group);

            //рассчет параметров
            foreach(var entry in statisticEntries)
            {
                entry.CalculateHeroSingleDps();
                entry.CalculateEnemyWantedLevel();
            }
            foreach (var statistic in _heroStatistics.Values) statistic.CalculateTotalDps();
            foreach (var entry in statisticEntries)
            {
                entry.CalculateEnemySingleDps();
                entry.CalculateHeroRiskLevel();
            }
            foreach (var statistic in _enemyStatistics.Values) statistic.CalculateTotalDps();

            //рассчет входящего урона
            foreach (var statistic in _heroStatistics.Values) statistic.CalculateIncomingDamage();
            foreach (var statistic in _enemyStatistics.Values) statistic.CalculateIncomingDamage();
        }

        public HeroBattleStatistic GetStatistic(LocationHeroData hero)
        {
            if (_heroStatistics.TryGetValue(hero, out var statistic))
                return statistic;
            return null;
        }
        public EnemyBattleStatistic GetStatistic(EnemyData enemy)
        {
            if (_enemyStatistics.TryGetValue(enemy, out var statistic))
                return statistic;
            return null;
        }
    }
}
