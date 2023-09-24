using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeObjectFactory : MonoBehaviour, IEnemyFactory
{
    #region PUBLIC_PROPERTIES

    public List<EnemyTypeObject> enemyTypes; // Lista de tipos de enemigos con sus configuraciones

    #endregion

    #region PUBLIC_METHODS

    public EnemyMovement CreateEnemy(Vector3 position, string enemyType)
    {
        EnemyTypeObject typeObject = GetEnemyTypeObject(enemyType);
        if (typeObject != null)
        {
            // Crea y configura un enemigo según el tipo
            GameObject enemyObject = Instantiate(typeObject.Prefab, position, Quaternion.identity);
            EnemyMovement enemyMovement = enemyObject.GetComponent<EnemyMovement>();
            enemyMovement.enemyType = typeObject; // Asigna el tipo de enemigo
            return enemyMovement;
        }
        else
        {
            Debug.LogError("Tipo de enemigo no encontrado: " + enemyType);
            return null;
        }
    }

    #endregion

    #region PRIVATE_METHODS

    private EnemyTypeObject GetEnemyTypeObject(string enemyType)
    {
        // Busca el tipo de enemigo por nombre en la lista de tipos de enemigos
        foreach (EnemyTypeObject typeObject in enemyTypes)
        {
            if (typeObject.TypeName == enemyType)
            {
                return typeObject;
            }
        }
        return null;
    }

    #endregion
}
