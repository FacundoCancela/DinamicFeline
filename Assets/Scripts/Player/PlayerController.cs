using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AttackController attackController;

    private void Start()
    {
        // Obtener una referencia al objeto del brazo (ArmController).
        attackController = GetComponentInChildren<AttackController>();
    }

    private void Update()
    {
        // Cambiar la estrategia de ataque según la tecla presionada.
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Cambiar a la estrategia de ataque básico.
            attackController.SetAttackStrategy(new BasicAttackStrategy());
            if (!attackController.IsAttacking)
            {
                attackController.PerformAttack();
            }
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            // Cambiar a la estrategia de ataque especial.
            attackController.SetAttackStrategy(new SpecialAttackStrategy());
            if (!attackController.IsAttacking)
            {
                attackController.PerformAttack();
            }
        }
    }
}
