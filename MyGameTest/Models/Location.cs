using MyGameTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameTest.Models
{
    public class Location : TimeObject
    {
        private IReadOnlyList<LocationLevel> _levels;
        private List<LocationHeroData> _heroes;
        private BattleCalculator _calculator;
        private bool _needRecalculate;

        public Location(IEnumerable<LocationLevel> levels)
        {
            _heroes = new List<LocationHeroData>();//TODO получить HeroData из сервиса и заполнить
            _levels = levels.ToList().AsReadOnly();
            CurrentLevel = _levels.FirstOrDefault();
        }

        public string Name { get; }

        public LocationLevel CurrentLevel 
        {
            get => _currentLevel;
            private set
            {
                if (_currentLevel == value) return;
                _currentLevel = value;
                if (_currentLevel == null) return;
                _calculator = new BattleCalculator(_heroes, _currentLevel.Enemies);
                _needRecalculate = true;
            }
        }
        private LocationLevel _currentLevel;

        protected override void Update(double deltaTime)
        {
            if (_needRecalculate)
            {
                _calculator.Calculate();
                _needRecalculate = false;
            }
            //TODO снижать HP и убивать юнитов
        }
    }
    public class BattleCalculator
    {
        private List<LocationHeroData> _heroes;
        private List<EnemyData> _enemies;
        public List<BattleStatistic> Statistics { get; } = new List<BattleStatistic>();

        public BattleCalculator(IEnumerable<LocationHeroData> heroes, IEnumerable<EnemyData> enemies)
        {
            _heroes = heroes.ToList();
            _enemies = enemies.ToList();
        }

        public void Calculate()
        {
            Statistics.Clear();

            //если героев нет, то врагов никто не бьет и рассчитывать нечего
            if (_heroes.All(x => x.Amount == 0))
            {
                _enemies.ForEach(x => x.IncomingDamagePerSec = 0);
                return;
            }                

            //создание статистики для участвующих героев. Остальных героев никто не бьет 
            foreach (var hero in _heroes.Where(x => x.Amount > 0))
            {
                if (hero.Amount > 0)
                    Statistics.Add(new BattleStatistic(hero, _enemies));
                else
                    hero.IncomingDamagePerSec = 0;
            }                

            //первый этап расчетов, который можно сделать внутри одного BattleStatistic
            Statistics.ForEach(x => x.Calculate());

            //второй этап расчетов для всех записей по каждому врагу внутри разных статистик 
            foreach (var enemy in _enemies)
            {
                var entries = Statistics.SelectMany(s => s.Entries.Where(e => e.Enemy.Equals(enemy))).ToList();
                var totalRisk = entries.Sum(x => x.HeroRiskLevel);
                foreach (var entry in entries)
                {
                    entry.EnemyPreference = entry.HeroRiskLevel / totalRisk;
                    entry.CalculateEnemyTotalDps();
                }

                //подсчет входящего урона со всех героев
                enemy.IncomingDamagePerSec = entries.Sum(x => x.HeroTotalDps);
            }

            //подсчет входящего урона со всех врагов для каждого героя
            foreach(var statistic in Statistics)
                statistic.Hero.IncomingDamagePerSec = statistic.Entries.Sum(x => x.EnemyTotalDps);
        }
    }
    public class BattleStatistic
    {
        public BattleStatistic(LocationHeroData hero, IEnumerable<EnemyData> enemies)
        {
            Hero = hero;
            Entries = enemies.Select(x => new BattleStatisticEntry(Hero, x)).ToList();
        }
        
        public void Calculate()
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

        public LocationHeroData Hero { get; }
        public List<BattleStatisticEntry> Entries { get; }
    }
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
    public class LocationLevel
    {
        public LocationLevel(int level, IEnumerable<EnemyData> enemies)
        {
            Level = level;
            Enemies = enemies.ToList().AsReadOnly();
        }

        public int Level { get; }
        public IReadOnlyList<EnemyData> Enemies { get; }
    }
    public class LocationHeroData
    {
        public LocationHeroData(HeroData data)
        {
            Data = data;
            CurrentHP = Data.HP;
        }

        public HeroData Data { get; }
        public int Amount { get; set; }
        public double CurrentHP { get; set; }
        public double IncomingDamagePerSec { get; set; } = 0;
    }
    public class HeroData : UnitData
    {
        public double AggroLevel { get; }
        public event Action DataChanged;
    }
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
    public class UnitData
    {
        public string Name { get; }
        public int Damage { get; }
        public double AttackDelay { get; }
        public int Armor { get; }
        public int HP { get; }
    }
}
