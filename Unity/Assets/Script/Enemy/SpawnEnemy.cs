using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public EnemyPool enemyPool;
    public int spawnCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            enemyPool.GetEnemy(transform.position);
        }
    }
}
