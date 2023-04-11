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

    [SerializeField] private StatsVariableIncreaser VaribleIncreaser;

    [Header("Private Variables")]
    private Vector3 startPoint;

    void Start()
    {
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
            case EnemyType.BASIC:
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
                }
                break;
            case EnemyType.TANK:
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

                    }
                }
                break;

            case EnemyType.EXPLOSIVE:
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

                    }
                }
                break;


        }

        enemy.SetActive(true);
        enemy.transform.position = startPosition;

        switch (type)
        {
            //Set specifications
            case EnemyType.BASIC:
                {
                    enemy.GetComponentInChildren<EnemyBehaviour>()._EnemyType = EnemyType.BASIC;
                    enemy.GetComponentInChildren<EnemyBehaviour>()._BasicAnimator.SetActive(true);
                    enemy.GetComponentInChildren<EnemyBehaviour>().HealthBar.SetActive(true);
                    enemy.GetComponentInChildren<EnemyBehaviour>().transform.localScale = new Vector3(1.5f, 1.5f, 0);
                    enemy.GetComponentInChildren<EnemyBehaviour>()._Speed = VaribleIncreaser.Basic_Speed * VaribleIncreaser._SpeedMultiplier;
                    enemy.GetComponentInChildren<EnemyBehaviour>()._MaxtHealth = VaribleIncreaser.Basic_MaxHealth * VaribleIncreaser._HealthMultiplier;
                    enemy.GetComponentInChildren<EnemyBehaviour>()._Health = enemy.GetComponentInChildren<EnemyBehaviour>()._MaxtHealth;
                    enemy.gameObject.GetComponentInChildren<EnemyBehaviour>()._HealthBarController.resetHeath();
                    break;
                }
            case EnemyType.TANK:
                {
                    enemy.GetComponentInChildren<EnemyBehaviour>()._EnemyType = EnemyType.TANK;
                    enemy.GetComponentInChildren<EnemyBehaviour>()._TankAnimator.SetActive(true);
                    enemy.GetComponentInChildren<EnemyBehaviour>().HealthBar.SetActive(true);
                    enemy.GetComponentInChildren<EnemyBehaviour>().transform.localScale = new Vector3(2, 2, 0); 
                    enemy.GetComponentInChildren<EnemyBehaviour>()._Speed = VaribleIncreaser.Tank_Speed * VaribleIncreaser._SpeedMultiplier;
                    enemy.GetComponentInChildren<EnemyBehaviour>()._MaxtHealth = VaribleIncreaser.Tank_MaxHealth * VaribleIncreaser._HealthMultiplier;
                    enemy.GetComponentInChildren<EnemyBehaviour>()._Health = enemy.GetComponentInChildren<EnemyBehaviour>()._MaxtHealth;
                    enemy.gameObject.GetComponentInChildren<EnemyBehaviour>()._HealthBarController.resetHeath();
                    break;
                }
            case EnemyType.EXPLOSIVE:
                {
                    enemy.GetComponentInChildren<EnemyBehaviour>()._EnemyType = EnemyType.EXPLOSIVE;
                    enemy.GetComponentInChildren<EnemyBehaviour>()._ExplodingAnimator.SetActive(true);
                    enemy.GetComponentInChildren<EnemyBehaviour>().HealthBar.SetActive(true);
                    enemy.GetComponentInChildren<EnemyBehaviour>()._Speed = VaribleIncreaser.Explosive_Speed * VaribleIncreaser._SpeedMultiplier;
                    enemy.GetComponentInChildren<EnemyBehaviour>()._MaxtHealth = VaribleIncreaser.Explosive_MaxHealth * VaribleIncreaser._HealthMultiplier;
                    enemy.GetComponentInChildren<EnemyBehaviour>()._Health = enemy.GetComponentInChildren<EnemyBehaviour>()._MaxtHealth;
                    enemy.gameObject.GetComponentInChildren<EnemyBehaviour>()._HealthBarController.resetHeath();
                    break;
                }

        }
        return enemy;

    }



    public void ReturnEnemy(GameObject enemy, EnemyType type)
    {
        enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //levelManager.ListOfEnemies.Remove(enemy.GetComponent<EnemyBehaviour>());
        enemy.SetActive(false);

        switch (type)
        {
            case EnemyType.BASIC:
                {
                    enemyPool.Enqueue(enemy);
                    //stats
                    enemyCount = enemyPool.Count;
                    ActiveEnemies--;
                }
                break;
            case EnemyType.TANK:
                {
                    enemyPool.Enqueue(enemy);
                    //stats
                    enemyCount = enemyPool.Count;
                    ActiveEnemies--;
                }
                break;
            case EnemyType.EXPLOSIVE:
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

