public class DamagePowerUp : PowerUp
{
    public int damageBonus = 2;

    public override void ApplyPowerUp(Stats characterStats)
    {
        characterStats.damage += damageBonus;
    }
}
