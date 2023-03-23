using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [Range(10f, 120f)]
    [SerializeField] public float intermission = 60f;
    [SerializeField] int enemyKillsCounter = 0;

    [SerializeField] private float spawnRate = 2f;
    [SerializeField] public int waveGoal;
    [SerializeField] int enemyOnMap = 0;
    [SerializeField] public int waveNumber = 1;
    [SerializeField] private GameObject PowerUpUI;
    public static SpawnManager Instance;
    public bool intermissionOn = false;
    

    private EnemyManager enemyManager;
    int poolCount;
    private EnemyType enemyType;
    
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] public bool isEndless = false;
    [SerializeField] private bool canSpawnEnemy = true;
    [SerializeField] private Transform[] Basic_ESpawnLocations;
    [SerializeField] private Transform[] Tank_ESpawnLocations;
    [SerializeField] private Transform[] Explosive_ESpawnLocations;
    [SerializeField]public bool isPowerUpSelected = false;
    [SerializeField] private StatsVariableIncreaser VaribleIncreaser;
    [SerializeField] int _BasicEnemyCount, _TankEnemyCount, _ExplosiveEnemyCount;

    private void Awake()
    {
        Instance = this;   
    }
    void Start()
    {
        waveGoal = _BasicEnemyCount + _TankEnemyCount + _ExplosiveEnemyCount;
        enemyManager = FindObjectOfType<EnemyManager>();
        StartCoroutine(BasicEnemySpanwer());
        StartCoroutine(TankEnemySpanwer());
        StartCoroutine(ExplosiveEnemySpanwer());
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
        VaribleIncreaser.WaveRound++;

        if (waveNumber ==  2)
        {
            VaribleIncreaser._SpeedMultiplier = VaribleIncreaser._SpeedMultiplier + 0.1f;
            VaribleIncreaser._HealthMultiplier = VaribleIncreaser._HealthMultiplier + 0.2f;

            Debug.Log(VaribleIncreaser._SpeedMultiplier);
            Debug.Log(VaribleIncreaser._HealthMultiplier);

        }
        if (waveNumber == 10)
        {
            VaribleIncreaser._SpeedMultiplier = VaribleIncreaser._SpeedMultiplier + 0.1f;
            VaribleIncreaser._HealthMultiplier = VaribleIncreaser._HealthMultiplier + 0.2f;

        }

        _BasicEnemyCount = (int)(_BasicEnemyCount * 1.50f);
        _TankEnemyCount = (int)(_TankEnemyCount * 1.50f);
        _ExplosiveEnemyCount = (int)(_ExplosiveEnemyCount * 1.50f);
        waveGoal = _BasicEnemyCount + _TankEnemyCount + _ExplosiveEnemyCount;
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

        StartCoroutine(BasicEnemySpanwer());
        StartCoroutine(TankEnemySpanwer());
        StartCoroutine(ExplosiveEnemySpanwer());
    }
    public void NotifyKill()
    {
        enemyKillsCounter++;

    }
    private IEnumerator BasicEnemySpanwer()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while(canSpawnEnemy && !isEndless)
        {
            yield return wait;
            int locationIndex = 0;
            for (int i = 0;  i < _BasicEnemyCount ; i++)
            {
                if(locationIndex > 3)
                {
                    locationIndex = 0;
                    yield return wait;
                }
                    enemyType = EnemyType.BASIC;
                    enemyManager.GetEnemy(Basic_ESpawnLocations[locationIndex].position, enemyType);
               
                    locationIndex++;
                    enemyOnMap++;
                
                    if(enemyOnMap >= _BasicEnemyCount)// For future modes  == Keeping track of data;
                {
                    canSpawnEnemy=false;
                }
            }

            
        }//Waves

    }
    private IEnumerator TankEnemySpanwer()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawnEnemy && !isEndless)
        {
            yield return wait;
            int locationIndex = 0;
            for (int i = 0; i < _TankEnemyCount; i++)
            {
                if (locationIndex > 3)
                {
                    locationIndex = 0;
                    yield return wait;
                }
                enemyType = EnemyType.TANK;
                enemyManager.GetEnemy(Tank_ESpawnLocations[locationIndex].position, enemyType);

                locationIndex++;
                enemyOnMap++;

                if (enemyOnMap >= _TankEnemyCount)// For future modes  == Keeping track of data;
                {
                    canSpawnEnemy = false;
                }
            }


        }//Waves
     
        
    }
    private IEnumerator ExplosiveEnemySpanwer()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawnEnemy && !isEndless)
        {
            yield return wait;
            int locationIndex = 0;
            for (int i = 0; i < _ExplosiveEnemyCount; i++)
            {
                if (locationIndex > 3)
                {
                    locationIndex = 0;
                    yield return wait;
                }
                enemyType = EnemyType.EXPLOSIVE;
                enemyManager.GetEnemy(Explosive_ESpawnLocations[locationIndex].position, enemyType);

                locationIndex++;
                enemyOnMap++;

                if (enemyOnMap >= _ExplosiveEnemyCount)// For future modes  == Keeping track of data;
                {
                    canSpawnEnemy = false;
                }
            }


        }


    }
}
