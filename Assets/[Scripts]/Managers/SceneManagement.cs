using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] Button Waves, Endless, Settings, Instructions;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject GameScene;
    [SerializeField] GameObject comingSoon;
    [SerializeField] GameObject menuScene;
    [SerializeField] GameObject InitialScene, subMenu;
    [SerializeField] GameObject StageSelect;
    public void PlayEndless()
    {
        comingSoon.SetActive(true);
        StartCoroutine(DeactivateCommingSoon());
    }
    IEnumerator DeactivateCommingSoon()
    {
        yield return new WaitForSeconds(1);
        comingSoon.SetActive(false);
    }
    public void PlayWaves()
    {
        StageSelect.SetActive(true);
        subMenu.SetActive(false);
   
    }
    public void PlayLevel1()
    {
        menuScene.SetActive(false);
        StageSelect.SetActive(false);
        InitialScene.SetActive(true);
        GameScene.SetActive(true);
    }
}
