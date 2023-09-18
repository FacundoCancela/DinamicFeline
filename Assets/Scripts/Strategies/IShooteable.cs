using System.Collections;
using UnityEngine;

public interface IShooteable
{
    bool CanFire { get; set; }      // Bool que indica si puede disparar
    int RemainingAmmo { get; set; } // Variable que cuenta la cantidad de balas restantes

    void Fire(); // Método para disparar
}
