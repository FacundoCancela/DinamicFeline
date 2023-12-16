using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottles : MonoBehaviour
{
    [SerializeField] private CharacterStats playerHealth;
    [SerializeField] private PlayerMovement saltando;
    private bool playerDamaged;
    private int damage;

    private void Start()
    {
        damage = 10;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!playerDamaged && !saltando.Saltando)
            {
                Debug.Log("player dañado");
                playerHealth.TakeDamage(damage); 
                playerDamaged = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerDamaged = false;
        }
    }

}
