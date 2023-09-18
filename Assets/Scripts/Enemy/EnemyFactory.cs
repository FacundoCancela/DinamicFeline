using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IEnemyFactory
{
    public GameObject enemyPrefab; // Asigna el prefab del enemigo básico desde el Inspector.

    public EnemyMovement CreateEnemy(Vector3 position)
    {
        if (enemyPrefab != null)
        {
            // Crea y configura un enemigo tipo zombie.
            GameObject enemyObject = Instantiate(enemyPrefab, position, Quaternion.identity);
            EnemyMovement enemy = enemyObject.GetComponent<EnemyMovement>();
            // Configura otras propiedades del enemigo si es necesario.
            return enemy;
        }
        else
        {
            Debug.LogError("Prefab de enemigo no asignado en la fábrica de enemigos.");
            return null;
        }
    }
}
