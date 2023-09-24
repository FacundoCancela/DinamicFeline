using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public string powerUpName;

    public abstract void ApplyPowerUp(Stats characterStats);
}
