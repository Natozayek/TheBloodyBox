using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] Button Waves, Endless, Exit, Rank;
    public void PlayEndless()
    {
        SceneManager.LoadScene("Endless");
    }
    public void PlayWaves()
    {
        SceneManager.LoadScene("Waves");
    }
}
