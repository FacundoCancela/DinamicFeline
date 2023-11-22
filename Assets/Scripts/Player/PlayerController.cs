using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public AttackController attackController;
    public BlockController blockController;
    public WeaponManager weaponManager;
    public bool haveAWeapon = false;

    Animator animator;
    [SerializeField] float attackCooldown = 0;
    AudioManager_Character audioManager;

    public TextMeshProUGUI ammoDisplay;
    private ICommand basicAttackCommand;
    private ICommand specialAttackCommand;

    private void Start()
    {
        // Inicializa los comandos concretos.
        basicAttackCommand = new BasicAttackCommand(attackController);
        specialAttackCommand = new SpecialAttackCommand(attackController);
        ammoDisplay.text = "0";
        animator = GetComponent<Animator>();
        audioManager = GetComponentInChildren<AudioManager_Character>();

    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (!haveAWeapon)
        {
            // Cambiar la estrategia de ataque según la tecla presionada, solo si no estamos bloqueando.
            if (!blockController.IsBlocking)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    if (attackCooldown <= 0)
                    {
                        audioManager.AS_basicAttack.Play();
                        animator.SetTrigger("Punch");

                        // Ejecuta el comando de ataque básico cuando se presiona la tecla O.
                        basicAttackCommand.Execute();
                        attackCooldown = 0.4f;
                    }

                }
                else if (Input.GetKeyDown(KeyCode.I))
                {
                    //audioManager.AS_specialAttack.Play();
                    if (attackCooldown <= 0)
                    {
                        animator.SetTrigger("SpecialAttack");
                        // Ejecuta el comando de ataque especial cuando se presiona la tecla I.
                        specialAttackCommand.Execute();
                        attackCooldown = 0.4f;
                    }

                }

                else if (Input.GetKeyDown(KeyCode.L))
                {

                    GameObject pistol = GameObject.Find("Pistol(Clone)");
                    if (pistol != null)
                    {
                        ammoDisplay.text = pistol.GetComponent<Pistol>().RemainingAmmo.ToString();
                        if (pistol.GetComponent<Pistol>().RemainingAmmo > 0 && pistol.GetComponent<Pistol>().CanFire)
                        {

                            animator.SetTrigger("Shoot");
                            
                        }
                    }


                }
            }

            // Bloquear/desbloquear mientras se mantiene presionada la tecla C.
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (!blockController.IsBlocking)
                {
                    // Cambiar a la estrategia de defensa básica (bloqueo).
                    blockController.SetDefenseStrategy(new BasicDefense());
                    blockController.PerformBlock();
                }
                else
                {
                    // Liberar el bloqueo (desbloquear).
                    blockController.ReleaseBlock();
                }
            }
        }

    }

    void Shoot()
    {
        GameObject pistol = GameObject.Find("Pistol(Clone)");
        if (pistol != null)
        {
            pistol.GetComponent<Pistol>().Fire();
        }
    }

}
