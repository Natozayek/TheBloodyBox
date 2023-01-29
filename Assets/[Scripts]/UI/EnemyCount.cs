using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class EnemyCount : MonoBehaviour
{   public static EnemyCount Instance;
    [SerializeField]private Text enemyKCText;

    private int enemyKC;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;// In first scene, make us the singleton.
        }
        else if (Instance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }

   public void EnemyKilled()
    {
        enemyKC++;
        enemyKCText.text = "EnemyKilled + " + enemyKC.ToString() ;
    }

}
