using MyGameTest.Models;

namespace MyGameTest.ViewModels
{
    public class HeroStatisticEntryViewModel : StatisticEntryViewModel
    {
        public HeroStatisticEntryViewModel(EnemyData enemy)
        {
            Name = enemy.Name;
        }

        public override string Name { get; }

        protected override double SingleDps => _entry?.HeroSingleDps ?? 0;
        protected override double MyPreference => _entry?.HeroPreference ?? 0;
        protected override double TotalDps => _entry?.HeroTotalDps ?? 0;
    }
}
