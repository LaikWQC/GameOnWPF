using MyGameTest.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameTest.Models
{
    public class Location : TimeObject
    {
        private IReadOnlyList<LocationLevel> _levels;
        private List<LocationHeroData> _heroes = new List<LocationHeroData>();
        private BattleCalculator _calculator;
        private bool _needRecalculate;

        public Location(IEnumerable<LocationLevel> levels)
        {
            var heroService = ServiceLocator.Current.GetService<HeroService>();

            foreach(var hero in heroService.Heroes)
                _heroes.Add(new LocationHeroData(hero));
            _heroes.ForEach(x => x.AmountChanged += () => _needRecalculate = true);
            _heroes.ForEach(x => x.Data.DataChanged += () => _needRecalculate = true);

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
                CreateCalculator();
            }
        }
        private LocationLevel _currentLevel;

        private void CreateCalculator()
        {
            _calculator = new BattleCalculator(_heroes, Enemies);
            _needRecalculate = true;
        }

        protected override void Update(double deltaTime)
        {
            if (_needRecalculate)
            {
                _calculator.Calculate();
                _needRecalculate = false;
            }

            _heroes.ForEach(x => x.TakeDamage(deltaTime));
            //foreach (var enemy in Enemies) enemy.TakeDamage(deltaTime);
        }

        public IReadOnlyCollection<LocationHeroData> Heroes => _heroes.AsReadOnly();
        public IReadOnlyCollection<EnemyData> Enemies => _currentLevel?.Enemies;
    }
}
