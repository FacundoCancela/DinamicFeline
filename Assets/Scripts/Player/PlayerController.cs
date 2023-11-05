using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public AttackController attackController;
    public BlockController blockController;
    public WeaponManager weaponManager;
    public bool haveAWeapon = false;

    Animator animator;



    private ICommand basicAttackCommand;
    private ICommand specialAttackCommand;

    private void Start()
    {
        // Inicializa los comandos concretos.
        basicAttackCommand = new BasicAttackCommand(attackController);
        specialAttackCommand = new SpecialAttackCommand(attackController);

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!haveAWeapon)
        {
            // Cambiar la estrategia de ataque según la tecla presionada, solo si no estamos bloqueando.
            if (!blockController.IsBlocking)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    animator.SetTrigger("Punch");
                    // Ejecuta el comando de ataque básico cuando se presiona la tecla O.
                    basicAttackCommand.Execute();

                }
                else if (Input.GetKeyDown(KeyCode.I))
                {
               
                    // Ejecuta el comando de ataque especial cuando se presiona la tecla I.
                    specialAttackCommand.Execute();
                }

                else if (Input.GetKeyDown(KeyCode.L))
                {
  
                        animator.SetTrigger("Shoot");   
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
}
