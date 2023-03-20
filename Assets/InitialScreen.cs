using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialScreen : MonoBehaviour
{
    [SerializeField] GameObject InitialScene;

    private void Start()
    {
        InitialScene.SetActive(true);
    }

}
