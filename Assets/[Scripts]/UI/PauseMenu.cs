using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject GamePausedScreen, Enemy_Encyclopedia;

    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        GamePausedScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        GamePausedScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        GamePausedScreen.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }
    public void Enemies()
    {
        GamePausedScreen.SetActive(false);
        Enemy_Encyclopedia.SetActive(true);
    }
    public void CloseEEncyclopedia()
    {
        Enemy_Encyclopedia.SetActive(false);
        GamePausedScreen.SetActive(true);
    }
    
    public void QuitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();

    }

}
