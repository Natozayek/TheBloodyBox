using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [Range(10f, 120f)]
    [SerializeField] public float intermission = 60f;
    [SerializeField] int enemyKillsCounter = 0;
    [Range(4.0f, 100f)]
    [SerializeField] public int waveGoal;
    [SerializeField] int enemyOnMap = 0;
    public int waveNumber = 1;
    [SerializeField] private GameObject PowerUpUI;
    public static SpawnManager Instance;
    public bool intermissionOn = false;
    

    private EnemyManager enemyManager;
    public EnemyType enemyType;
    public int poolCount;
    

    
    [Range(1, 100)]
    public int enemyNumber = 1;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] public bool isEndless = false;
    [SerializeField] private bool canSpawnEnemy = true;
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField]public bool isPowerUpSelected = false;

    private void Awake()
    {
        Instance = this;   
    }
    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        StartCoroutine(Spawner());
    }

    private void Update()
    {
        
        if (enemyKillsCounter >= waveGoal)// Set waves 
        {
            intermissionOn = true;
            UIManager.instance.IntermissionTimer.gameObject.SetActive(true);
            intermission = intermission - Time.deltaTime;

            if(intermission <= 41 && !isPowerUpSelected)
            {
                PowerUpUI.gameObject.SetActive(true);
            }

            if (intermission < 0f)
            {
                SetNextWave();
            }
            UIManager.instance.DecreaseTimer(intermission);
        }
    }

    private void SetNextWave()
    {
        //Reset enemy counters
        enemyKillsCounter = 0;
        enemyOnMap = 0;

        //Wave specifications 
        UIManager.instance.IncreaseWaves();
        waveNumber++;
        waveGoal = waveGoal + waveGoal/2;
        UIManager.instance.IncreaseGoalNumber(waveGoal);
        UIManager.instance.ResetEnemiesKilled();
        UIManager.instance.IntermissionTimer.gameObject.SetActive(false);
        intermission = 60;
        UIManager.instance.IntermissionTimerN = intermission;
        isPowerUpSelected = false;

        //Release enemies
        canSpawnEnemy = true;
        intermissionOn = false;
        
        //Increase Enemy stats for next wave
        poolCount = enemyManager.enemyPool.Count;
        Debug.Log(poolCount);

        for (int i = 0; i < poolCount; i++)
        {
            if (enemyManager.enemyPool.ElementAt(i).gameObject != null)
            {
                enemyManager.enemyPool.ElementAt(i).gameObject.GetComponentInChildren<EnemyBehaviour>()._MaxtHealth += enemyManager.enemyPool.ElementAt(i).gameObject.GetComponentInChildren<EnemyBehaviour>()._MaxtHealth * 0.1f; ;
                enemyManager.enemyPool.ElementAt(i).gameObject.GetComponentInChildren<EnemyBehaviour>()._Health = enemyManager.enemyPool.ElementAt(i).gameObject.GetComponentInChildren<EnemyBehaviour>()._MaxtHealth;
                enemyManager.enemyPool.ElementAt(i).gameObject.GetComponentInChildren<EnemyBehaviour>()._Speed += enemyManager.enemyPool.ElementAt(i).gameObject.GetComponentInChildren<EnemyBehaviour>()._Speed * 0.04f;
               // = (int)enemyManager.enemyPool.ElementAt(i).gameObject.GetComponentInChildren<EnemyBehaviour>().maxtHealth;
            }
            //Debug.Log(enemyManager.enemyPool.ElementAt(i).gameObject.CompareTag("Enemy"));// += enemyManager.enemyPool.ElementAt(i).gameObject.GetComponent<EnemyBehaviour>().maxtHealth * 0.2f;
            //enemyManager.enemyPool.ElementAt(i).gameObject.GetComponent<EnemyBehaviour>().speed += enemyManager.enemyPool.ElementAt(i).gameObject.GetComponent<EnemyBehaviour>().speed * 0.2f;
        }

        for (int i = 0; i < poolCount; i++)
        {
            enemyManager.enemyPool.ElementAt(i).gameObject.GetComponentInChildren<EnemyHealthBarController>().resetHeath();

        }


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
            int locationIndex = 0;
            Debug.Log(waveGoal);
            for (int i = 0;  i < waveGoal ; i++)
            {
                if(locationIndex > 3)
                {
                    locationIndex = 0;
                    yield return wait;
                }
                    enemyManager.GetEnemy(spawnLocations[locationIndex].position, enemyType);
               
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
            //  int rand = Random.Range(0, enemyList.Count);
            int locationIndex = 0;
            for (int i = 0; i < spawnLocations.Length; i++)
            {
                enemyManager.GetEnemy(spawnLocations[locationIndex].position, enemyType);

                locationIndex++;
                enemyOnMap++;

                if (enemyOnMap >= waveGoal)// For future modes  == Keeping track of data;
                {
                    canSpawnEnemy = false;
                }
            }


        }//Endless
    }
}
