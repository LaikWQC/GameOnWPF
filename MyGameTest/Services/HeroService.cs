using MyGameTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyGameTest.Services
{
    public class HeroService : IService
    {
        public void Initialize(IEnumerable<HeroData> heroes)
        {
            Heroes = heroes.ToList().AsReadOnly();
        }

        public IReadOnlyList<HeroData> Heroes { get; private set; }
    }
}
