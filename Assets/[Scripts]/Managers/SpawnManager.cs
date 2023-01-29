using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Range(1, 100)]
    public int enemyNumber = 50;

    [SerializeField]private List<GameObject> enemyList;
    [SerializeField]private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private bool canSpawnEnemy = true;
    [SerializeField] private Transform[] spawnLocations;
    // Start is called before the first frame update
    void Start()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        BuildEnemyList();

        StartCoroutine(Spawner());
    }
    public void BuildEnemyList()
    {
        enemyList = new List<GameObject>();

        for (var i = 0; i < enemyNumber; i++)
        {
            var enemy = enemyPrefab;
            enemyList.Add(enemy);
            //enemy.SetActive(false);
        }
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while(canSpawnEnemy)
        {
            yield return wait;
            int rand = Random.Range(0, enemyList.Count);
            GameObject enemy = enemyList[rand];
            Instantiate(enemy, spawnLocations[Random.Range(0, spawnLocations.Length)].position, Quaternion.identity);
            Debug.Log("Spawning");
        }
    }
}
