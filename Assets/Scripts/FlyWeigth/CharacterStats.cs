using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stats stats; // Referencia al Scriptable Object de estad�sticas.
    private List<IPlayerDeathObserver> playerDeathObservers = new List<IPlayerDeathObserver>();

    private int currentHealth; // La salud actual del personaje.
    private int damage;
    private float speed;

    private void Start()
    {
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

    // M�todo para recibir da�o.
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }

        // Mostrar la vida actual del personaje en el Debug.Log.
        Debug.Log(gameObject.name + " Vida Actual: " + currentHealth);
    }

    // M�todo para aplicar un power-up de vida.
    public void ApplyHealthPowerUp(int healthAmount)
    {
        currentHealth += healthAmount;

        // Asegurarse de que la salud no supere el l�mite m�ximo.
        currentHealth = Mathf.Min(currentHealth, stats.health);
    }

    // M�todo para aplicar un power-up de da�o.
    public void ApplyDamagePowerUp(int damageBoost)
    {
        damage += damageBoost;
    }

    // M�todo para aplicar un power-up de velocidad.
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
