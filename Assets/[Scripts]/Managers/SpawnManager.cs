using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [Range(10f, 120f)]
    [SerializeField] public float intermission = 60f;
    [SerializeField] int enemyKillsCounter = 0;

    [SerializeField] private float spawnRate = 7f;
    [SerializeField] public int waveGoal = 7;
    [SerializeField] int enemyOnMap = 0;
    [SerializeField] public int waveNumber = 1;
    [SerializeField] private GameObject PowerUpUI;
    public static SpawnManager Instance;
    public bool intermissionOn = false;
    public bool GameOverOn = false;


    private EnemyManager enemyManager;
    int poolCount;
    private EnemyType enemyType;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] public bool isEndless = false;
    [SerializeField] private bool canSpawnEnemy = true;
    [SerializeField] private Transform[] Basic_ESpawnLocations;
    [SerializeField] private Transform[] Tank_ESpawnLocations;
    [SerializeField] private Transform[] Explosive_ESpawnLocations;
    [SerializeField] public bool isPowerUpSelected = false;
    [SerializeField] private StatsVariableIncreaser VaribleIncreaser;
    [SerializeField] int _BasicEnemyCount, _TankEnemyCount, _ExplosiveEnemyCount;
    [SerializeField] GameObject _LoadingScreen;
    [SerializeField] GameObject _WinScreen;

    PlayerBehaviour _playerBehaviour;

    private void Awake()
    {
        Instance = this;
    }


    public void InitializeSpawn()
    {
        canSpawnEnemy = true;
        waveGoal = _BasicEnemyCount + _TankEnemyCount + _ExplosiveEnemyCount;
        enemyManager = FindObjectOfType<EnemyManager>();
        StartCoroutine(BasicEnemySpanwer());
        StartCoroutine(TankEnemySpanwer());
        StartCoroutine(ExplosiveEnemySpanwer());
        Debug.Log("INitializeSpawn");
    }

    private void Update()
    {

        if (enemyKillsCounter >= waveGoal && waveNumber == 10)
        {
            GameOverOn = true;
            _WinScreen.SetActive(true);
        }

        if (enemyKillsCounter >= waveGoal && !GameOverOn && waveNumber <=9)// Set waves 
        {
            intermissionOn = true;
            UIManager.instance.IntermissionTimer.gameObject.SetActive(true);
            intermission = intermission - Time.deltaTime;

            if (intermission <= 51 && !isPowerUpSelected)
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

        if (waveNumber == 2)
        {
            VaribleIncreaser._EnemySpeedMultiplier = VaribleIncreaser._EnemySpeedMultiplier + 0.07f;
            VaribleIncreaser._EnemyHealthMultiplier = VaribleIncreaser._EnemyHealthMultiplier + 0.2f;

            Debug.Log(VaribleIncreaser._EnemySpeedMultiplier);
            Debug.Log(VaribleIncreaser._EnemyHealthMultiplier);

        }
        if (waveNumber == 10)
        {
            VaribleIncreaser._EnemySpeedMultiplier = VaribleIncreaser._EnemySpeedMultiplier + 0.1f;
            VaribleIncreaser._EnemyHealthMultiplier = VaribleIncreaser._EnemyHealthMultiplier + 0.2f;

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
    public void ResetVaribles()
    {
        //Reset enemy counters
        enemyKillsCounter = 0;
        enemyOnMap = 0;


        waveNumber = 1;
        VaribleIncreaser.WaveRound = 1;
        VaribleIncreaser._EnemySpeedMultiplier = 1;
        VaribleIncreaser._EnemyHealthMultiplier = 1;
        VaribleIncreaser._Bullet_Damage_Multiplier = 1;
        VaribleIncreaser._BulletSpeed_Multiplier = 1;

        _playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        _playerBehaviour.ResetVariables();
        _BasicEnemyCount = 3;
        _TankEnemyCount = 2;
        _ExplosiveEnemyCount = 2;
        waveGoal = _BasicEnemyCount + _TankEnemyCount + _ExplosiveEnemyCount; 

        UIManager.instance.IncreaseGoalNumber(waveGoal);
        UIManager.instance.ResetEnemiesKilled();
        //Open Loading Screen;
        
        _LoadingScreen.SetActive(true);
        _LoadingScreen.GetComponent<LoadingBarController>().StartLoading();
        //Initialize Timer
        GameOverOn = false;

    

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
