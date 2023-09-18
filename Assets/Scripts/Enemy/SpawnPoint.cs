using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public EnemyFactory enemyFactory; // Asigna la f�brica de enemigos en el Inspector.
    public float detectionRadius = 5f;
    public int generatedEnemies = 0;
    public int maxEnemies = 5;
    public float spawnOffset = 1.0f; // Distancia entre cada enemigo.
    public LayerMask playerLayer;

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player") && generatedEnemies < maxEnemies)
            {
                // El jugador se acerca al SpawnPoint, crea un enemigo.
                CreateEnemy();
                generatedEnemies++;
                // Puedes ajustar la frecuencia de creaci�n de enemigos aqu� si es necesario.
                // Tambi�n puedes agregar l�gica para limitar la cantidad de enemigos generados.
            }
        }
    }

    private void CreateEnemy()
    {
        if (enemyFactory != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(spawnOffset * generatedEnemies, 0f, 0f);
            enemyFactory.CreateEnemy(spawnPosition);
        }
    }
}
