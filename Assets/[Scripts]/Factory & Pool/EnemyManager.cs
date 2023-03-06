using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Properties")]
    [Range(10, 1000)]
    [SerializeField] public Queue<GameObject> enemyPool =  new Queue<GameObject>();
    public int numberOfEnemies = 100;
    public int enemyCount = 0;
    public int ActiveEnemies = 0;

    private Factory factory;
   

    [Header("Private Variables")]
    private Vector3 startPoint;

    void Start()
    {
        //enemyPool = new Queue<GameObject>(); // creates an empty queue container
        factory = GameObject.FindObjectOfType<Factory>();
        BuildEnemyPool();
    }
    void BuildEnemyPool()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            enemyPool.Enqueue(factory.CreateEnemy());

        }
        enemyCount = enemyPool.Count;

    }


    public GameObject GetEnemy(Vector3 startPosition, EnemyType type )
    {
        GameObject enemy = null;
        
        switch (type)
        {
            case EnemyType.Enemy1:
                {
                    if (enemyPool.Count < 1)
                    {
                        enemyPool.Enqueue(factory.CreateEnemy());
                    }
                    {
                        enemy = enemyPool.Dequeue();
                        // stats
                        enemyCount = enemyPool.Count;
                        ActiveEnemies++;
                    }

                   // enemy.GetComponent<EnemyBehaviour>().enemType = type;
                }
                break;
            case EnemyType.Enemy2:
                {
                    {
                        if (enemyPool.Count < 1)
                        {
                            enemyPool.Enqueue(factory.CreateEnemy());
                        }
                        {
                            enemy = enemyPool.Dequeue();
                            // stats
                            enemyCount = enemyPool.Count;
                            ActiveEnemies++;
                        }

                      //  enemy.GetComponent<EnemyBehaviour>().enemType = type;
                    }
                }
                break;

        }

        enemy.SetActive(true);
        enemy.transform.position = startPosition;
        return enemy;
        //switch (type)
        //{
        //    case BulletType.SPIRAL:
        //        threeSixtyAngleShoot(startPosition, bullet, angle);
        //        break;

        //    case BulletType.SINGLE:
        //        AimShoot(bullet, angle);
        //        break;

        //    case BulletType.SHOTGUN:
        //        SpreadShot(targetPos, bullet, angle);
        //        break;

        //}
        //return bullet;
    }



    public void ReturnEnemy(GameObject enemy, EnemyType type)
    {
        enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        enemy.SetActive(false);

        switch (type)
        {
            case EnemyType.Enemy1:
                {
                    enemyPool.Enqueue(enemy);
                    //stats
                    enemyCount = enemyPool.Count;
                    ActiveEnemies--;
                }
                break;
            case EnemyType.Enemy2:
                {
                    enemyPool.Enqueue(enemy);
                    //stats
                    enemyCount = enemyPool.Count;
                    ActiveEnemies--;
                }
                break;
          
        }
    }




}

