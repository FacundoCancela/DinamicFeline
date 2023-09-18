using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stats stats; // Referencia al Scriptable Object de estadísticas.

    private int currentHealth; // La salud actual del personaje.
    private int damage;

    private void Start()
    {
        // Inicializa la salud actual con el valor del Scriptable Object.
        currentHealth = stats.health;
        damage = stats.damage;
    }

    // Método para recibir daño.
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }

        // Mostrar la vida actual del enemigo en el Debug.Log.
        Debug.Log(gameObject.name + " Vida Actual: " + currentHealth);
    }

    public int DoDamage()
    {
        return damage;
    }

    private void Die()
    {
        if(gameObject.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " ha muerto.");
        }
        else
        {
            Debug.Log(gameObject.name + " ha muerto.");
            Destroy(gameObject);
        }
    }
}
