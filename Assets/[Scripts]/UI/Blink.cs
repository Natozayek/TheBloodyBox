using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float fadeSpeed;
    [SerializeField] GameObject Logo;
    [SerializeField] GameObject Menu;
    bool isMenu_Open;
    [SerializeField]float i;
    private void Start()
    {
        StartCoroutine(FadeImage(true));
    }
    private void Update()
    {
        if(isMenu_Open)
        {
            i = i - Time.deltaTime/10f;

            if (i <= 0)
            {
                Menu.SetActive(true);
                Logo.SetActive(false);
                gameObject.SetActive(false);
            }
            else
            {
                Logo.GetComponent<Image>().color = new Color(0.264f, 0.264f, 0.264f, i);
                gameObject.GetComponent<Image>().color = new Color(0.264f, 0.264f, 0.264f, i);
            }
           


        }
    }
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway || isMenu_Open)
        {
            // loop over 1 second backwards
            for (float i = 0.9f; i >= 0.0f; i -= Time.deltaTime)
            {
                // set color with i as alpha
                gameObject.GetComponent<Image>().color= new Color(0.264f, 0.264f, 0.264f, i);
                yield return null;
            }
            fadeAway = false;
        }
        // fade from transparent to opaque
        if(!fadeAway) 
        {
            // loop over 1 second
            for (float i = 0.0f; i <= 1f; i += Time.deltaTime)
            {
                // set color with i as alpha
                gameObject.GetComponent<Image>().color = new Color(0.264f, 0.264f, 0.264f, i);
                yield return null;
            }
        }
        yield return new WaitForSeconds(fadeSpeed);
        StartCoroutine(FadeImage(true));
    }

    public void ActivateMenu()
    {
        isMenu_Open = true;
    }
}