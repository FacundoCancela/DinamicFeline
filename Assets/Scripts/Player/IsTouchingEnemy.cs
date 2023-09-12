using UnityEngine;

public class IsTouchingEnemy : MonoBehaviour
{
    private GameObject touchedEnemy; // Referencia al último enemigo tocado.
    public AttackController attackController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            touchedEnemy = collision.gameObject;
            attackController.HandleEnemyCollision();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            touchedEnemy = null; // Cuando el jugador ya no toca al enemigo, borra la referencia.
            attackController.HandleEnemyCollision();
        }
    }

    public GameObject GetTouchedEnemy()
    {
        return touchedEnemy;
    }
}
