using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackCommand : ICommand
{
    private AttackController attackController;

    public BasicAttackCommand(AttackController controller)
    {
        attackController = controller;
    }

    public void Execute()
    {
        // Realiza la acción de ataque básico.
        attackController.SetAttackStrategy(new BasicAttack());
        if (!attackController.IsAttacking)
        {
            attackController.PerformAttack();
        }
    }
}