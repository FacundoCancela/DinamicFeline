using UnityEngine;

public class PowerUpBox : MonoBehaviour
{
    public PowerUp powerUpPrefab; // Asigna el prefab del power-up que se aplicar�.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUpToPlayer(other.gameObject);
            Destroy(gameObject); // Destruye la caja de power-up.
        }
    }

    private void ApplyPowerUpToPlayer(GameObject player)
    {
        if (powerUpPrefab != null)
        {
            // Crea una instancia del power-up.
            PowerUp powerUpInstance = Instantiate(powerUpPrefab);

            // Intenta acceder al componente CharacterStats del jugador.
            CharacterStats playerStats = player.GetComponent<CharacterStats>();
            Debug.Log(playerStats);
            if (playerStats != null)
            {
                // Obt�n las estad�sticas del jugador.
                Stats playerStatsData = playerStats.stats;

                // Aplica el power-up al jugador usando sus estad�sticas.
                powerUpInstance.ApplyPowerUp(playerStatsData);

                playerStats.UpdateStats();

                // Destruye el power-up despu�s de aplicarlo.
                Destroy(powerUpInstance.gameObject);
            }
            else
            {
                Debug.LogWarning("El jugador no tiene el componente CharacterStats.");
            }
        }
    }
}
