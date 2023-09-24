public class SpeedPowerUp : PowerUp
{
    public float speedBonus = 2f;

    public override void ApplyPowerUp(Stats characterStats)
    {
        characterStats.speed += speedBonus;
    }
}
