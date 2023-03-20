using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBarController : MonoBehaviour
{

    public Image BarFill;
    bool isLoaded = false;
    [SerializeField] bool isMenuLoading;
    [SerializeField] bool isGameLoading = false;
    [SerializeField] GameObject CountDownObject;
    [SerializeField] GameObject GameUI;
    private void Start()
    {
        StartLoading();
    }
    public void StartLoading()
    {
        StartCoroutine(Loading());
    }
    IEnumerator Loading()
    {
        float progressValue = 0.0f;
        while (!isLoaded)
        {

           
            BarFill.fillAmount = progressValue;

            if(BarFill.fillAmount>= 1)
            {
                isLoaded = true;
                if (CountDownObject != null && GameUI != null)
                { 
                    CountDownObject.SetActive(true);
                    GameUI.SetActive(true);
                    gameObject.SetActive(false);
                }
                if (CountDownObject == null || GameUI == null)
                {
                   
                    gameObject.SetActive(false);
                }

            }
            else
            {
                if(isMenuLoading)
                {
                    progressValue += 0.1f;
                }
                if(isGameLoading)
                {
                    progressValue += 0.05f;
                }
            }
            yield return new WaitForSeconds(0.25f);

        }
    }
}
