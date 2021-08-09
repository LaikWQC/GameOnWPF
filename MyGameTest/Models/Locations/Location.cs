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
}
