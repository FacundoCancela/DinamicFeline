using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private IAttackStrategy currentAttackStrategy;
    private bool isAttacking = false; // Variable para rastrear si se está realizando un ataque.
    public PlayerMovement playerMovement; // Referencia al PlayerMovement.

    public bool IsAttacking
    {
        get { return isAttacking; }
    }

    // Asigna la referencia al PlayerMovement en el Inspector.
    public PlayerMovement PlayerMovement
    {
        get { return playerMovement; }
        set { playerMovement = value; }
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

        currentAttackStrategy.Attack(transform, IsFacingRight());

        yield return new WaitForSeconds(currentAttackStrategy.GetAttackDuration());

        isAttacking = false; // Indicar que se ha completado el ataque.
    }

    private bool IsFacingRight()
    {
        // Verificar la dirección en la que el jugador está mirando usando la referencia a PlayerMovement.
        return playerMovement != null && playerMovement.isFacingRight;
    }
}
