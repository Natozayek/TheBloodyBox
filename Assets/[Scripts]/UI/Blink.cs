using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float fadeSpeed;
    private void Start()
    {
        StartCoroutine(FadeImage(true));
    }
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1f; i >= 0.5f; i -= Time.deltaTime)
            {
                // set color with i as alpha
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, i);
                yield return null;
            }
            fadeAway = false;
        }
        // fade from transparent to opaque
        if(!fadeAway) 
        {
            // loop over 1 second
            for (float i = 0.5f; i <= 1f; i += Time.deltaTime)
            {
                // set color with i as alpha
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        yield return new WaitForSeconds(fadeSpeed);
        StartCoroutine(FadeImage(true));
    }
}