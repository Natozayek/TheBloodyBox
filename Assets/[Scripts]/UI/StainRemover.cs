using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainRemover : MonoBehaviour
{
    [Range(0f, 60f)]
    [SerializeField] float timer;
    float alpha = 0.9f;
    private bool doOnce = false;
    [SerializeField] GameObject EnemyPrefab;

    float timeElapsed;
    float lerpDuration = 50;

    void Update()
    {

        
            if (SpawnManager.Instance.intermissionOn)
            {
                       if (timeElapsed < lerpDuration)
                       {
                           gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
                           alpha -= Time.deltaTime;
                           timeElapsed += Time.deltaTime;
                       }
                        else
                        {
                        EnemyPrefab.GetComponent<EnemyBehaviour>().ReturnEnemy();
                        }      
            }
    }



    //public IEnumerator FadeImage(bool fadeAway)
    //{
    //    // fade from opaque to transparent
    //    if (fadeAway)
    //    {  
    //        {
    //            // set color with i as alpha
    //            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
    //            alpha -= Time.deltaTime;
    //            Debug.Log(alpha);
             
    //        }
         
    //    }
    //    yield return new WaitForSeconds(1f);
    //    if (alpha <= 0.2f)
    //    {
   
    //    }
    //        StartCoroutine(FadeImage(true));
    //       // fade from transparent to opaque

    //}
}
