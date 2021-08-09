namespace MyGameTest.Models
{
    public class LocationHeroData
    {
        public LocationHeroData(HeroData data)
        {
            Data = data;

            CurrentHP = Data.HP;
            IncomingDamagePerSec = 0;
        }

        public HeroData Data { get; }
        public int Amount { get; set; }
        public double CurrentHP { get; set; }
        public double IncomingDamagePerSec { get; set; } 
    }
}
