using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stats stats; // Referencia al Scriptable Object de estadísticas.
    private List<IPlayerDeathObserver> playerDeathObservers = new List<IPlayerDeathObserver>();

    private int currentHealth; // La salud actual del personaje.
    private int damage;
    private float speed;

    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer del hijo.
    private Color originalColor; // Almacena el color original del sprite.

    private void Start()
    {
        // Obtén la referencia al componente SpriteRenderer del hijo.
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Almacena el color original del sprite.
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        UpdateStats();

        var playerDeathObserver = new PlayerDeathObserver();

        // Registrar el observador en CharacterStats
        RegisterPlayerDeathObserver(playerDeathObserver);
    }

    public void UpdateStats()
    {
        currentHealth = stats.health;
        damage = stats.damage;
        speed = stats.speed;
    }

    // Método para recibir daño.
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Cambiar temporalmente el color del sprite a rojo.
            StartCoroutine(FlashSpriteColor(Color.red, 0.2f)); // Cambia el color a rojo durante 0.2 segundos.
        }

        // Mostrar la vida actual del personaje en el Debug.Log.
        Debug.Log(gameObject.name + " Vida Actual: " + currentHealth);
    }

    // Corutina para destellar el color del sprite.
    private IEnumerator FlashSpriteColor(Color flashColor, float duration)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = flashColor; // Cambiar el color del sprite a rojo.

            yield return new WaitForSeconds(duration); // Esperar la duración especificada.

            // Restaurar el color original del sprite.
            spriteRenderer.color = originalColor;
        }
    }

    // Método para aplicar un power-up de vida.
    public void ApplyHealthPowerUp(int healthAmount)
    {
        currentHealth += healthAmount;

        // Asegurarse de que la salud no supere el límite máximo.
        currentHealth = Mathf.Min(currentHealth, stats.health);
    }

    // Método para aplicar un power-up de daño.
    public void ApplyDamagePowerUp(int damageBoost)
    {
        damage += damageBoost;
    }

    // Método para aplicar un power-up de velocidad.
    public void ApplySpeedPowerUp(float speedBoost)
    {
        speed += speedBoost;
    }

    public int DoDamage()
    {
        return damage;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void RegisterPlayerDeathObserver(IPlayerDeathObserver observer)
    {
        playerDeathObservers.Add(observer);
    }

    private void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            foreach (var observer in playerDeathObservers)
            {
                observer.OnPlayerDeath();
            }
        }
        else
        {
            Debug.Log(gameObject.name + " ha muerto.");
            PointManager.Instance.AddPoints(10);
            Destroy(gameObject);
        }
    }
}
