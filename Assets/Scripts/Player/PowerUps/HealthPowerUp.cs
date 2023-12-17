using UnityEngine;
public class HealthPowerUp : PowerUp
{
    public int healthBonus = 20;
    private float _movementSpeed = 3;
    DropPower_Up dropPower_Up;
    Vector3 nodoPos;
    private void Update()
    {

        transform.position += Vector3.down * _movementSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, nodoPos.y, 10f), transform.position.z);
    }
    public override void ApplyPowerUp(Stats characterStats)
    {
        characterStats.health += healthBonus;
    }

    public override void getObjectivePosition(Vector3 position)
    {
        nodoPos = position;
    }
}
