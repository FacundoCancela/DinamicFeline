using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AttackController attackController;
    public BlockController blockController;
    public WeaponManager weaponManager;
    public bool haveAWeapon = false;

    private void Update()
    {
        if(!haveAWeapon)
        {
            // Cambiar la estrategia de ataque según la tecla presionada, solo si no estamos bloqueando.
            if (!blockController.IsBlocking)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    // Cambiar a la estrategia de ataque básico.
                    attackController.SetAttackStrategy(new BasicAttack());
                    if (!attackController.IsAttacking)
                    {
                        attackController.PerformAttack();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.I))
                {
                    // Cambiar a la estrategia de ataque especial.
                    attackController.SetAttackStrategy(new SpecialAttack());
                    if (!attackController.IsAttacking)
                    {
                        attackController.PerformAttack();
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
}
