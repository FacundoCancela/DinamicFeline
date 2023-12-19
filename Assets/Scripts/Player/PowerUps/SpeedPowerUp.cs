using UnityEngine;
public class SpeedPowerUp : PowerUp
{
    public float speedBonus = 2f;
    private float _movementSpeed = 3;
    Vector3 nodoPos;
    DropPower_Up dropPower_Up;
    private void Update()
    {
      
        transform.position += Vector3.down * _movementSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, nodoPos.y, 10f), transform.position.z);
    }
    public override void ApplyPowerUp(Stats characterStats)
    {
        characterStats.speed += speedBonus;
    }

    public override void getObjectivePosition(Vector3 position)
    {
        nodoPos = position;
    }
}
