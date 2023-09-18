using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    public IShooteable currentWeapon;
    public Transform playerHand; // Asigna la mano del jugador desde el inspector.
    public WeaponManager weaponManager;

    private void Start()
    {
        // Buscar el componente IShooteable en el hijo de este objeto.
        currentWeapon = GetComponentInChildren<IShooteable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            weaponManager.playerHaveWeapon = true;

            if (currentWeapon != null)
            {
                // Instanciar el arma en la mano del jugador.
                Instantiate(currentWeapon as MonoBehaviour, playerHand.position, playerHand.rotation, playerHand);
            }

            Destroy(gameObject);
        }
    }
}
