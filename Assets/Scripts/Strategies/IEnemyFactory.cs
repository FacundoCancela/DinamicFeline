using UnityEngine;
public interface IEnemyFactory
{
    EnemyMovement CreateEnemy(Vector3 position, string enemyType);
}
