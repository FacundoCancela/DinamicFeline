using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackCommand : ICommand
{
    private AttackController attackController;

    public SpecialAttackCommand(AttackController controller)
    {
        attackController = controller;
    }

    public void Execute()
    {
        // Realiza la acción de ataque especial.
        attackController.SetAttackStrategy(new SpecialAttack());
        if (!attackController.IsAttacking)
        {
            attackController.PerformAttack();
        }
    }
}