using MyGameTest.Data;
using System;

namespace MyGameTest.Models
{
    public class HeroData : UnitData
    {
        public HeroData(HeroDataDto dto) : base(dto)
        {
            AggroLevel = dto.AggroLevel;
        }

        public double AggroLevel { get; }
        public event Action DataChanged;
    }
}
