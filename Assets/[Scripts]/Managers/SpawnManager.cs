using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [Range(10f, 120f)]
    [SerializeField] private float intermission = 0f;
    public int enemyKillsCounter = 0;
    [SerializeField]public int waveGoal;
    public int enemyOnMap = 0;
    private int waveNumber;
    public static SpawnManager Instance;

    public bool isEndless = false;
    [Range(1, 100)]
    public int enemyNumber = 1;

    [SerializeField]private List<GameObject> enemyList;
    [SerializeField]private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private bool canSpawnEnemy = true;
    [SerializeField] private Transform[] spawnLocations;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;   
    }
    void Start()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        BuildEnemyList();
        waveNumber = 1;
        StartCoroutine(Spawner());
    }
    public void BuildEnemyList()
    {
        enemyList = new List<GameObject>();

        for (var i = 0; i < enemyNumber; i++)
        {
            var enemy = enemyPrefab;
            enemyList.Add(enemy);
      
        }
    }
    private void Update()
    {
        //Make 3 2 1 timer to start wave 1; With are you ready button
        //Accumulate points to improve fire rate, bullet damage in initial weapon (pistol single), weapons drop ( spiral, shotgun) + needing 3 more (grenades, bazooka, mele last)
        

        if (enemyKillsCounter >= waveGoal)// Set waves 
        {
            intermission = intermission + Time.deltaTime;

            if (intermission > 20f)
            {
                SetNextWave();
            }
        
            Debug.Log(intermission);
            //Destroy all enemies in the scene
            //Give a minute to upgrade do things
            //set new goal
            //Decrease the spawnRate
            //Increase goal for next wave


        }
    }

    private void SetNextWave()
    {
        enemyKillsCounter = 0;
        enemyOnMap = 0;
        waveNumber++;
        waveGoal = waveGoal + 10;
        canSpawnEnemy = true;
        intermission = 0;
        //Increase stats of enemies
        StartCoroutine(Spawner());
    }

    public void NotifyKill()
    {
        enemyKillsCounter++;

    }
    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while(canSpawnEnemy && !isEndless)
        {
            yield return wait;
            int rand = Random.Range(0, enemyList.Count);
            GameObject enemy = enemyList[rand];
            int locationIndex = 0;
            for (int i = 0;  i < spawnLocations.Length ; i++)
            {
             
                    Instantiate(enemy, spawnLocations[locationIndex].position, Quaternion.identity);
                    locationIndex++;
                    enemyOnMap++;
                
                    if(enemyOnMap >= waveGoal)// For future modes  == Keeping track of data;
                {
                    canSpawnEnemy=false;
                }
            }

            
        }
        while (canSpawnEnemy && isEndless)
        {
            yield return wait;
            int rand = Random.Range(0, enemyList.Count);
            GameObject enemy = enemyList[rand];
            int locationIndex = 0;
            for (int i = 0; i < spawnLocations.Length; i++)
            {

                Instantiate(enemy, spawnLocations[locationIndex].position, Quaternion.identity);
                locationIndex++;
                enemyOnMap++;

                if (enemyOnMap >= waveGoal)// For future modes  == Keeping track of data;
                {
                    canSpawnEnemy = false;
                }
            }


        }
    }
}
