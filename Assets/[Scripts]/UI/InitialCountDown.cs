using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialCountDown : MonoBehaviour
{
    public Text  CountDown;
    public bool isLoaded = false;
    [SerializeField] SpawnManager spawner;

    

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
            Debug.Log(CountDownStart);
            if (CountDownStart <= 0)
            {
                
                isLoaded = true;
                spawner.enabled = true;
                spawner.InitializeSpawn();
                isLoaded = false;
                CountDown.text = "";
                CountDownStart = 3.0f;
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
