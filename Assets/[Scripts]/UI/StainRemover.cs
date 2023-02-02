using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainRemover : MonoBehaviour
{
    [Range(0f, 60f)]
    [SerializeField] float timer;
    float alpha = 0.9f;
    private bool doOnce = false;

    private void Update()
    {
        if(SpawnManager.Instance.intermissionOn)
        {
            if(doOnce == false)
            {
                StartCoroutine(FadeImage(true));
                doOnce = true;
            }
           
        }
    }


    public IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {  
            {
                // set color with i as alpha
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
                alpha -= Time.deltaTime;
                Debug.Log(alpha);
             
            }
         
        }
        yield return new WaitForSeconds(1f);
        if (alpha <= 0.1f)
        {
            Destroy(this.gameObject);
        }
        StartCoroutine(FadeImage(true));
        // fade from transparent to opaque

    }
}
