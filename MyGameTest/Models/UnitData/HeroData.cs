using System;

namespace MyGameTest.Models
{
    public class HeroData : UnitData
    {
        public double AggroLevel { get; }
        public event Action DataChanged;
    }
}
