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

        protected override double GetSingleDps => _entry?.HeroSingleDps ?? 0;
    }
}
