using UnityEngine;

public class DamagePowerUp : PowerUp
{
    public int damageBonus = 2;
    private float _movementSpeed = 3;
    private float nodeMinPos;
    Vector3 nodoPos;
    DropPower_Up dropPower_Up;
    private void Update()
    {

        transform.position += Vector3.down * _movementSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, nodoPos.y, 10f), transform.position.z);
    }

    public override void ApplyPowerUp(Stats characterStats)
    {
        characterStats.damage += damageBonus;
    }


    public override void getObjectivePosition(Vector3 position)
    {
       
    }
}
