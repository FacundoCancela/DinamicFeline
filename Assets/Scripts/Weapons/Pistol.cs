using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Pistol : MonoBehaviour, IShooteable
{
    public bool CanFire { get; set; } = true;
    public int RemainingAmmo { get; set; } = 10;

    public Transform WeaponTransform;
    public GameObject BulletPrefab;
    public WeaponManager weaponManager;
    Animator animator;
    AudioSource audio;
    public TextMeshProUGUI ammoDisplay;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        CanFire = weaponManager.playerHaveWeapon;


       

        if(RemainingAmmo <= 0)
        {
            Destroy(gameObject);
            weaponManager.playerHaveWeapon = false;
      
        }
   
        }

    public void Fire()
    {

        if (CanFire && RemainingAmmo > 0)
        {
            audio.Play();
            if (BulletPrefab != null && WeaponTransform != null)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                Instantiate(BulletPrefab, WeaponTransform.position, WeaponTransform.rotation);
                RemainingAmmo--;

                GetComponentInParent<PlayerController>().ammoDisplay.text = RemainingAmmo.ToString();

            }

        }
    }

    public void Bullet()
    {

    }
}
