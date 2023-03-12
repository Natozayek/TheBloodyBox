using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialCountDown : MonoBehaviour
{
    public Text  CountDown;
    bool isLoaded = false;
    [SerializeField] SpawnManager spawner;
    
    private void Start()
    {
      StartCountDown();
    }
    public void StartCountDown()
    {
        StartCoroutine(Loading());
    }
    IEnumerator Loading()
    {
        float CountDownStart = 3.0f;
        CountDown.text = "Prepare to fight!";
        yield return new WaitForSeconds(2);
        while (!isLoaded)
        {
            CountDown.text = CountDownStart.ToString();
            if (CountDownStart <= 0)
            {
                isLoaded = true;
                spawner.enabled = true;
                gameObject.SetActive(false);

            }
            else
            {
                CountDownStart--;
                
            }
            yield return new WaitForSeconds(1f);

        }
    }
}
