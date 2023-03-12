using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarController : MonoBehaviour
{

    public Image BarFill;
    bool isLoaded = false;
    [SerializeField] GameObject CountDownObject;
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
                CountDownObject.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                progressValue += 0.05f;
            }
            yield return new WaitForSeconds(0.25f);

        }
    }
}
