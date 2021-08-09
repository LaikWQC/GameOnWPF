using System.Collections.Generic;
using System.Linq;

namespace MyGameTest.Models
{
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
}
