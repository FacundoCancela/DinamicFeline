using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAttackController : MonoBehaviour
{
    private IAttackStrategy currentAttackStrategy;
    public EnemyMovement enemyMovement; // Referencia al EnemyMovement.
    public IsTouchingPlayer isTouchingPlayer;
    public CharacterStats characterStats;
    public EnemyTypeObject enemyType;
    private bool isAttacking = false; // Variable para rastrear si se está realizando un ataque.
    private bool canAttack = true; // Variable para controlar si el enemigo puede atacar nuevamente.
    private bool playerDamaged = false;
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

    public void HandlePlayerCollision()
    {
        if (!playerDamaged)
        {
            // Obtén el GameObject del último enemigo tocado.
            GameObject playerObject = isTouchingPlayer.GetTouchedPlayer();
            if (playerObject != null)
            {
                // Comprueba si el objeto tiene un componente CharacterStats.
                CharacterStats playerStats = playerObject.GetComponent<CharacterStats>();

                if (playerStats != null && IsAttacking)
                {
                    // Obtén el daño del ataque actual y aplícalo al enemigo.
                    int damage = currentAttackStrategy.GetDamage();
                    playerStats.TakeDamage(damage * characterStats.DoDamage());
                    playerDamaged = true;
                }
            }
        }
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
        isAttacking = true; // Indicar que se está atacando.

        currentAttackStrategy.Attack(transform, IsFacingRight());

        DamagePlayer();

        yield return new WaitForSeconds(currentAttackStrategy.GetAttackDuration());

        isAttacking = false; // Indicar que se ha completado el ataque.
        playerDamaged = false;

        // Iniciar el temporizador de intervalo entre ataques.
        attackTimer = 0f;
    }



    private bool IsFacingRight()
    {
        // Verificar la dirección en la que el enemigo está mirando usando la referencia a EnemyMovement.
        return enemyMovement != null && enemyMovement.isFacingRight;
    }

    void Update()
    {
        // Actualizar el temporizador de intervalo entre ataques.
        attackTimer += Time.deltaTime;

        // Comprobar si el jugador está cerca según la variable playerIsClose en EnemyMovement.
        if (enemyMovement != null && enemyMovement.playerIsClose)
        {
            // Si el jugador está cerca y el temporizador ha alcanzado el intervalo, realiza un ataque.
            if (attackTimer >= attackInterval)
            {
                PerformAttack();
                canAttack = false; // Desactivar la posibilidad de atacar nuevamente.
            }
        }

        if(attackTimer >= attackInterval)
        {
            // Si el jugador no está cerca, permitir que el enemigo pueda atacar nuevamente.
            canAttack = true;
        }
    }
}
