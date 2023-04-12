using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainManager : MonoBehaviour
{
    [SerializeField] GameObject _LoadingScreen, _GameScene, _GameOverScreen, _GameUI, _CountDownTimer;
    SpawnManager _SpawnManager;

  
       
        



    public void PlayAgain()
    {
        _SpawnManager = FindObjectOfType<SpawnManager>();

        if(_SpawnManager != null )
        {
            _GameOverScreen.SetActive(false);
            _SpawnManager.ResetVaribles();

        }
        else
        {
            Debug.Log("NULL");
        }
        //Remove all enemies, Reset Variables, Pop the Loading Screen Start Timer, Start Spanwer


    }
}
