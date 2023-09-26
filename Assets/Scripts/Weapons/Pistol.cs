using System.Collections;
using UnityEngine;

public class Pistol : MonoBehaviour, IShooteable
{
    public bool CanFire { get; set; } = true;
    public int RemainingAmmo { get; set; } = 10;

    public Transform WeaponTransform;
    public GameObject BulletPrefab;
    public WeaponManager weaponManager;


    void Start()
    {
    }

    private void Update()
    {
        CanFire = weaponManager.playerHaveWeapon;


        if (CanFire && RemainingAmmo > 0)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Fire();
            }
        }

        if(RemainingAmmo <= 0)
        {
            Destroy(gameObject);
            weaponManager.playerHaveWeapon = false;
        }
    }

    public void Fire()
    {
        if (BulletPrefab != null && WeaponTransform != null)
        {
            Instantiate(BulletPrefab, WeaponTransform.position, WeaponTransform.rotation);
            RemainingAmmo--;
        }
    }
}
