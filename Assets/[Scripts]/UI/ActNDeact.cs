using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActNDeact : MonoBehaviour
{
    float timer;
    [SerializeField] GameObject nameHolder;

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        
        if(timer > 0.3f)
        {
            nameHolder.SetActive(false);
        }
        if (timer > 0.6)
        {
            nameHolder.SetActive(true);
            timer = 0;
        }
        
    }
}
