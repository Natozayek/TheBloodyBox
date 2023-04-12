using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainManager : MonoBehaviour
{
    [SerializeField] GameObject _LoadingScreen, _GameScene, _GameOverScreen, _WinScreen, _GameUI, _CountDownTimer;
    SpawnManager _SpawnManager;
    UIManager _UIManager;

  
       
        



    public void PlayAgain()
    {
        _SpawnManager = FindObjectOfType<SpawnManager>();
        _UIManager = FindObjectOfType<UIManager>();
        if (_SpawnManager != null && _UIManager != null)
        {
            _GameOverScreen.SetActive(false);
            _WinScreen.SetActive(false);
            _SpawnManager.ResetVaribles();
            _UIManager.Initialize();


        }
        else
        {
            Debug.Log("NULL");
        }
        //Remove all enemies, Reset Variables, Pop the Loading Screen Start Timer, Start Spanwer


    }
}
