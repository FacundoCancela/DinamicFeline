using UnityEngine;
using System.Collections.Generic;

public class BossSpawnPoint : MonoBehaviour
{
    public EnemyTypeObjectFactory enemyFactory; // Asigna la fábrica de enemigos en el Inspector
    public float detectionRadius = 5f; //rango de deteccion del jugador
    public float spawnOffset = 1.0f; // Distancia entre cada enemigo
    public LayerMask playerLayer; //layer del jugador

    private List<string> enemyTypes = new List<string> { "Boss1" }; // Tipos de enemigos
    private List<string> generatedEnemies = new List<string>(); // Almacena los tipos de enemigos generados en orden
    public int maxEnemies = 1; // Número total de enemigos a generar

    void Start()
    {
        // Genera la lista de enemigos en orden aleatorio
        GenerateRandomEnemyList();
    }

    private void GenerateRandomEnemyList()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            string randomEnemyType = GetRandomEnemyType();
            generatedEnemies.Add(randomEnemyType);
        }
    }

    private string GetRandomEnemyType()
    {
        // Obtiene un tipo de enemigo aleatorio de la lista de tipos disponibles
        int randomIndex = Random.Range(0, enemyTypes.Count);
        return enemyTypes[randomIndex];
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player") && generatedEnemies.Count > 0)
            {
                // Obtiene el próximo tipo de enemigo en la lista y lo crea
                string nextEnemyType = generatedEnemies[0];
                CreateEnemy(nextEnemyType);
                generatedEnemies.RemoveAt(0);
            }
        }
    }

    private void CreateEnemy(string enemyType)
    {
        if (enemyFactory != null && !string.IsNullOrEmpty(enemyType))
        {
            Vector3 spawnPosition = transform.position + new Vector3(spawnOffset, 0f, 0f);
            enemyFactory.CreateEnemy(spawnPosition, enemyType);
            spawnOffset += 3;
        }
    }
}
