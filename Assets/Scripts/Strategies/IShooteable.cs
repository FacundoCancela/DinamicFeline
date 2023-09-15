using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooteable
{
    int Damage { get; set; }        // Variable de da�o
    bool CanFire { get; set; }      // Bool que indica si puede disparar
    int RemainingAmmo { get; set; } // Variable que cuenta la cantidad de balas restantes
    Transform WeaponTransform { get; set; } // Transform para la posici�n del arma
    GameObject BulletPrefab { get; set; }  // GameObject para las balas

    void Fire(); // M�todo para disparar
}