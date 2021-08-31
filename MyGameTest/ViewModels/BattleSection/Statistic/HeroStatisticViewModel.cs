using MyGameTest.Models;
using System.Linq;

namespace MyGameTest.ViewModels
{
    public class HeroStatisticViewModel : UnitStatisticViewModel
    {
        private LocationHeroData _hero;

        public HeroStatisticViewModel(HeroViewModel hero, Location location) : base(location)
        {
            _hero = hero.Model;

            FillEntries();
            RefreshEntries();
        }

        public override string Name => _hero.Data.Name;

        protected override int HP => _hero.Data.HP;
        protected override int Armor => _hero.Data.Armor;
        protected override int Attack => _hero.Data.Damage;
        protected override double AtkDelay => _hero.Data.AttackDelay;

        protected override void OnLocationLevelChanged() => FillEntries();
        protected override void OnCalculationChanged() => RefreshEntries();

        private void FillEntries()
        {
            Entries.Clear();
            foreach (var enemy in _location.Enemies)
                Entries.Add(new HeroStatisticEntryViewModel(enemy));
        }

        private void RefreshEntries()
        {
            var statistic = _location.GetStatistic(_hero);
            if (statistic == null)
            {
                foreach (var entry in Entries)
                    entry.IsActive = false;
                return;
            }
            for (int i = 0; i < statistic.Entries.Count; i++)
                Entries[i].UpdateEntry(statistic.Entries[i]);
        }
    }
}
