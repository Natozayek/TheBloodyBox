using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [Range(10f, 120f)]
    [SerializeField] public float intermission = 30f;
    public int enemyKillsCounter = 0;
    [SerializeField]public int waveGoal;
    public int enemyOnMap = 0;
    public int waveNumber;
    public static SpawnManager Instance;

    
    [Range(1, 100)]
    public int enemyNumber = 1;

    [SerializeField]private List<GameObject> enemyList;
    [SerializeField]private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] public bool isEndless = false;
    [SerializeField] private bool canSpawnEnemy = true;
    [SerializeField] private Transform[] spawnLocations;


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
            UIManager.instance.IntermissionTimer.gameObject.SetActive(true);
            
            intermission = intermission - Time.deltaTime;

            if (intermission < 0f)
            {
                SetNextWave();
            }
        
            UIManager.instance.DecreaseTimer(intermission);
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

        //Wave specifications 
        waveNumber++;
        UIManager.instance.IncreaseWaves();
        waveGoal = waveGoal + 10;
        UIManager.instance.IncreaseGoalNumber(waveGoal);
        UIManager.instance.ResetEnemiesKilled();

        //Release enemies
        canSpawnEnemy = true;
        intermission = 60;

        //Increase Enemy stats for next wave
        enemyPrefab.GetComponent<EnemyBehaviour>().maxtHealth += enemyPrefab.GetComponent<EnemyBehaviour>().maxtHealth * 0.2f;
        enemyPrefab.GetComponent<EnemyBehaviour>().speed += enemyPrefab.GetComponent<EnemyBehaviour>().speed * 0.2f;
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

            
        }//Waves
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
            }


        }//Endless
    }
}
