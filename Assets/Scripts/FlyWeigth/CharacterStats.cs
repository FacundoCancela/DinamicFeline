using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public Stats stats; // Referencia al Scriptable Object de estadísticas.
    private List<IPlayerDeathObserver> playerDeathObservers = new List<IPlayerDeathObserver>();
    public Slider slider;

    public int CurrentHealth => currentHealth;
    private int currentHealth; // La salud actual del personaje.
    private int damage;
    private float speed;

    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer del hijo.
    private Color originalColor; // Almacena el color original del sprite.

    EnemyMovement enemyMovement;

    Animator animator;
    AudioManager_Character audioManager;
    private void Start()
    {
        // Obtén la referencia al componente SpriteRenderer del hijo.
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioManager = GetComponent<AudioManager_Character>();

        enemyMovement = GetComponent<EnemyMovement>();

        // Almacena el color original del sprite.
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        UpdateStats();

        var playerDeathObserver = new PlayerDeathObserver();

        // Registrar el observador en CharacterStats
        RegisterPlayerDeathObserver(playerDeathObserver);
        SetHealthBar();

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

        audioManager.AS_hurt.Play();
        currentHealth -= damageAmount;
        if(slider) slider.value = currentHealth;

        animator.SetTrigger("Damaged");

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
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
        if (slider) slider.value = currentHealth;
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
            enemyMovement.Death();
        }
    }

    void SetHealthBar()
    {
        if (!gameObject.CompareTag("Player"))
        {
            //Si el objeto no es el jugador destruir componente slider para que no tire errores por todos lados
            Destroy(slider);
        }

        else
        {
            slider.maxValue = currentHealth;
            slider.value = slider.maxValue;
        }
    }
}
