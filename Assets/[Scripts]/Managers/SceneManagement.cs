using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] Button Waves, Endless, Settings, Instructions;
    [SerializeField] GameObject loadingScrean;

    private void Update()
    {
        
    }
    public void PlayEndless()
    {
        Debug.Log("Coming Soon");
    }
    public void PlayWaves()
    {
        gameObject.SetActive(false);
        loadingScrean.gameObject.SetActive(true);
        SceneManager.LoadScene("Waves");
        

    }
}
