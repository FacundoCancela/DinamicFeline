using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTypeObjectFactory : MonoBehaviour, IEnemyFactory
{
    #region PUBLIC_PROPERTIES

    public List<EnemyTypeObject> bossType; // Lista de tipos de enemigos con sus configuraciones

    #endregion

    #region PUBLIC_METHODS

    public EnemyMovement CreateEnemy(Vector3 position, string bossType)
    {
        EnemyTypeObject typeObject = GetBossTypeObject(bossType);
        if (typeObject != null)
        {
            // Crea y configura un enemigo según el tipo
            GameObject bossObject = Instantiate(typeObject.Prefab, position, Quaternion.identity);
            EnemyMovement enemyMovement = bossObject.GetComponent<EnemyMovement>();
            enemyMovement.enemyType = typeObject; // Asigna el tipo de enemigo
            return enemyMovement;
        }
        else
        {
            Debug.LogError("Tipo de enemigo no encontrado: " + bossType);
            return null;
        }
    }

    #endregion

    #region PRIVATE_METHODS

    private EnemyTypeObject GetBossTypeObject(string enemyType)
    {
        // Busca el tipo de enemigo por nombre en la lista de tipos de enemigos
        foreach (EnemyTypeObject typeObject in bossType)
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
