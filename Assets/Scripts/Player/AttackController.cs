using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private IAttackStrategy currentAttackStrategy;
    private bool isAttacking = false; // Variable para rastrear si se está realizando un ataque.

    public bool IsAttacking
    {
        get { return isAttacking; }
    }

    public void SetAttackStrategy(IAttackStrategy strategy)
    {
        currentAttackStrategy = strategy;
    }

    public void PerformAttack()
    {
        if (!isAttacking && currentAttackStrategy != null)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true; // Indicar que se está atacando.

        currentAttackStrategy.Attack(transform);

        yield return new WaitForSeconds(currentAttackStrategy.GetAttackDuration());

        isAttacking = false; // Indicar que se ha completado el ataque.
    }
}
