using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private IAttackStrategy currentAttackStrategy;
    public EnemyMovement enemyMovement; // Referencia al EnemyMovement.
    public IsTouchingPlayer isTouchingPlayer;
    private bool isAttacking = false; // Variable para rastrear si se est� realizando un ataque.
    private bool canAttack = true; // Variable para controlar si el enemigo puede atacar nuevamente.
    private float attackInterval = 3f; // Intervalo entre ataques en segundos.
    private float attackTimer = 0f;

    private void Start()
    {
        this.SetAttackStrategy(new EnemyBasicAttack());
    }

    public bool IsAttacking
    {
        get { return isAttacking; }
    }

    // Asigna la referencia al EnemyMovement en el Inspector.
    public EnemyMovement EnemyMovement
    {
        get { return enemyMovement; }
        set { enemyMovement = value; }
    }

    public void SetAttackStrategy(IAttackStrategy strategy)
    {
        currentAttackStrategy = strategy;
    }

    public void PerformAttack()
    {
        if (!isAttacking && canAttack && currentAttackStrategy != null)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    public void DamagePlayer()
    {
        if(isTouchingPlayer)
        {
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true; // Indicar que se est� atacando.

        currentAttackStrategy.Attack(transform, IsFacingRight());

        DamagePlayer();

        yield return new WaitForSeconds(currentAttackStrategy.GetAttackDuration());

        isAttacking = false; // Indicar que se ha completado el ataque.

        // Iniciar el temporizador de intervalo entre ataques.
        attackTimer = 0f;
    }



    private bool IsFacingRight()
    {
        // Verificar la direcci�n en la que el enemigo est� mirando usando la referencia a EnemyMovement.
        return enemyMovement != null && enemyMovement.isFacingRight;
    }

    void Update()
    {
        // Actualizar el temporizador de intervalo entre ataques.
        attackTimer += Time.deltaTime;

        // Comprobar si el jugador est� cerca seg�n la variable playerIsClose en EnemyMovement.
        if (enemyMovement != null && enemyMovement.playerIsClose)
        {
            // Si el jugador est� cerca y el temporizador ha alcanzado el intervalo, realiza un ataque.
            if (attackTimer >= attackInterval)
            {
                PerformAttack();
                canAttack = false; // Desactivar la posibilidad de atacar nuevamente.
            }
        }

        if(attackTimer >= attackInterval)
        {
            // Si el jugador no est� cerca, permitir que el enemigo pueda atacar nuevamente.
            canAttack = true;
        }
    }
}
