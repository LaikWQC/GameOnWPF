namespace MyGameTest.ViewModels
{
    public interface IUnit
    {
        string Name { get; }
        int Amount { get; set; }
        double HpPercentage { get; }
        string IncomingDamagePerSec { get; }
    }
}
