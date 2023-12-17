using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public string powerUpName;

    public abstract void ApplyPowerUp(Stats characterStats);

    public abstract void getObjectivePosition(Vector3 position);
}
