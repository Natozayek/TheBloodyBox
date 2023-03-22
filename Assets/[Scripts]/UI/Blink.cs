using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float fadeSpeed;
    [SerializeField] GameObject _Logo;
    [SerializeField] GameObject _Menu;
    [SerializeField] GameObject _BloodVFX, _BloodVFX2;
    [SerializeField] bool isButton;
    bool isMenu_Open;
    bool PointerEnter;
    [SerializeField]float i;
    private void Start()
    {
        isMenu_Open = true;
        if(!isButton)
        {
            StartCoroutine(FadeImage(true));
        }
   
    }
    private void Update()
    {
        if(!isMenu_Open)
        {
            i = i - Time.deltaTime/2f;

            if (i <= 0)
            {
                _Menu.SetActive(true);
                _Logo.SetActive(false);
                _BloodVFX.SetActive(false);
                _BloodVFX2.SetActive(false);
                 gameObject.SetActive(false);
          
            }
            else
            {
                _Logo.GetComponent<Image>().color = new Color(0.264f, 0.264f, 0.264f, i);
                gameObject.GetComponent<Image>().color = new Color(0.264f, 0.264f, 0.264f, i);
            }
           


        }
    }
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway && isMenu_Open)
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
        if(!fadeAway && isMenu_Open) 
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
        StartCoroutine(FadeImage(isMenu_Open));
    }
    IEnumerator FadeMenuButton(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 0.9f; i >= 0.0f; i -= Time.deltaTime)
            {
                // set color with i as alpha
                gameObject.GetComponent<Image>().color = new Color(0.264f, 0.264f, 0.264f, i);
                yield return null;
            }
            fadeAway = false;
        }
        // fade from transparent to opaque
        if (!fadeAway)
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
        StartCoroutine(FadeImage(PointerEnter));
    }
    public void ActivateMenu()
    {
        isMenu_Open = false;
    }
    public void FadeButton()
    {
        StartCoroutine(FadeMenuButton(true));
    }
    public void StopFadeButton()
    {
        PointerEnter = false;
    }
}