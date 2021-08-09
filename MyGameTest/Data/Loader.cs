using MyGameTest.Models;
using MyGameTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameTest.Data
{
    public static class Loader
    {
        public static void Load()
        {
            var heroes = new List<HeroData>
            {
                new HeroData(new HeroDataDto
                {
                    Name = "Knight",
                    HP = 500,
                    Armor = 3,
                    Damage = 10,
                    AttackDelay = 1.5,
                    AggroLevel = 2
                }),
                new HeroData(new HeroDataDto
                {
                    Name = "Archer",
                    HP = 400,
                    Armor = 1,
                    Damage = 15,
                    AttackDelay = 1.4,
                    AggroLevel = 1
                })
            };
            ServiceLocator.Current.GetService<HeroService>().Initialize(heroes);

            var locations = new List<Location>
            {
                new Location(new List<LocationLevel>
                {
                    new LocationLevel(1, new List<EnemyData>
                    {
                        new EnemyData(new EnemyDataDto
                        {
                            Name = "Wolf",
                            HP = 250,
                            Armor = 0,
                            Damage = 8,
                            AttackDelay = 1,
                            Amount = 20,
                            WantedLevel = 1
                        }),
                        new EnemyData(new EnemyDataDto
                        {
                            Name = "Skeleton",
                            HP = 350,
                            Armor = 5,
                            Damage = 10,
                            AttackDelay = 1.5,
                            Amount = 10,
                            WantedLevel = 1.5
                        })
                    })
                })
            };
            ServiceLocator.Current.GetService<LocationService>().Initialize(locations);
        }
    }
}
