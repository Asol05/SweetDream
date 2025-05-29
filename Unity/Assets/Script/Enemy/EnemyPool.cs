using Mono.CSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public EnemyAI enemyPrefab;
    public int poolSize = 25;

    private Queue<EnemyAI> enemypool = new Queue<EnemyAI>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize;i++)
        {
            EnemyAI enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.gameObject.SetActive(true);
            enemy.OnDeath = ReturnToPool;
            enemypool.Enqueue(enemy);
        }
    }

    public EnemyAI GetEnemy(Vector3 position)
    {
        if (enemypool.Count > 0)
        {
            EnemyAI enemy = enemypool.Dequeue();
            enemy.transform.position = position;
            enemy.gameObject.SetActive(true);
            return enemy;
        }
        return null;
    }

    private void ReturnToPool(EnemyAI enemy)
    {
        enemypool.Enqueue(enemy);
    }

}
