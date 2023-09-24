public class HealthPowerUp : PowerUp
{
    public int healthBonus = 20;

    public override void ApplyPowerUp(Stats characterStats)
    {
        characterStats.health += healthBonus;
    }
}
