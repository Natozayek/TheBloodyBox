using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] public Text WavesNumber, EnemiesKilled, GoalNumber, IntermissionTimer;
    public int WaveN, EnemiesN, GoalN;
    public float IntermissionTimerN;

    private void Awake()
    {
        instance = this;
    }
    void  Start()
    {
        Initialize();
    }
    public void Initialize()
    {
       

        WaveN = SpawnManager.Instance.waveNumber;
        Debug.Log(WaveN);
        if (SpawnManager.Instance.isEndless)
        {
            GoalN = 999999;
        }
        else
        {
            GoalN = SpawnManager.Instance.waveGoal;
        }
            
        EnemiesN = 0;
        IntermissionTimerN = SpawnManager.Instance.intermission;
        IntermissionTimer.gameObject.SetActive(false);

        UpdateWaves();
        UpdateEnemiesKilled();
        UpdateGoalNumber();
       

    }
    public void IncreaseWaves()
    {
        WaveN++;
        UpdateWaves();
    }
    public void UpdateWaves()
    {
        WavesNumber.text = $"{WaveN}";
    }

    public void IncreaseEnemiesKilled()
    {
        EnemiesN++;
        UpdateEnemiesKilled();
    }
    public void ResetEnemiesKilled()
    {
        EnemiesN = 0;
        UpdateEnemiesKilled();
    }
    public void UpdateEnemiesKilled()
    {
        EnemiesKilled.text = $"{EnemiesN}";
    }
    public void IncreaseGoalNumber(int increaser)
    {
        GoalN = increaser;
        UpdateGoalNumber();
    }
    public void UpdateGoalNumber()
    {
        GoalNumber.text = $"{GoalN}";

    }
    public void DecreaseTimer(float timer)
    {
        IntermissionTimerN = timer;
        UpdateTimer();

    }
    public void UpdateTimer()
    {
        IntermissionTimer.text = $"{IntermissionTimerN.ToString("F0")}";
    }
}
