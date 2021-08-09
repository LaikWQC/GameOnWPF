using MyGameTest.Data;

namespace MyGameTest.Models
{
    public abstract class UnitData
    {
        public UnitData(UnitDataDto dto)
        {
            Name = dto.Name;
            Damage = dto.Damage;
            AttackDelay = dto.AttackDelay;
            Armor = dto.Armor;
            HP = dto.HP;
        }

        public string Name { get; }
        public int Damage { get; }
        public double AttackDelay { get; }
        public int Armor { get; }
        public int HP { get; }
    }
}
