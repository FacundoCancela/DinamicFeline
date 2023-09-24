using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsTouchingPlayer : MonoBehaviour
{
    private GameObject touchedPlayer;
    public BasicEnemyAttackController enemyAttackController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            touchedPlayer = collision.gameObject;
            enemyAttackController.HandlePlayerCollision();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            touchedPlayer = null;
            enemyAttackController.HandlePlayerCollision();
        }
    }

    public GameObject GetTouchedPlayer()
    {
        return touchedPlayer;
    }
}
