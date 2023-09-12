using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private IAttackStrategy currentAttackStrategy;
    private bool isAttacking = false; // Variable para rastrear si se est� realizando un ataque.
    private bool enemyDamaged = false;
    public PlayerMovement playerMovement; // Referencia al PlayerMovement.
    public IsTouchingEnemy isTouchingEnemy; // Referencia al IsTouchingEnemy.
    public CharacterStats characterStats;

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

    public void HandleEnemyCollision()
    {
        if (!enemyDamaged)
        {
            // Obt�n el GameObject del �ltimo enemigo tocado.
            GameObject enemyObject = isTouchingEnemy.GetTouchedEnemy();
            if (enemyObject != null)
            {
                // Comprueba si el objeto tiene un componente CharacterStats.
                CharacterStats enemyStats = enemyObject.GetComponent<CharacterStats>();

                if (enemyStats != null && IsAttacking)
                {
                    // Obt�n el da�o del ataque actual y apl�calo al enemigo.
                    int damage = currentAttackStrategy.GetDamage();
                    enemyStats.TakeDamage(damage * characterStats.DoDamage());
                    enemyDamaged = true;
                }
            }
        }
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
        isAttacking = true; // Indicar que se est� atacando.

        currentAttackStrategy.Attack(transform, IsFacingRight());

        yield return new WaitForSeconds(currentAttackStrategy.GetAttackDuration());

        isAttacking = false; // Indicar que se ha completado el ataque.
        enemyDamaged = false;
    }

    private bool IsFacingRight()
    {
        // Verificar la direcci�n en la que el jugador est� mirando usando la referencia a PlayerMovement.
        return playerMovement != null && playerMovement.isFacingRight;
    }
}
