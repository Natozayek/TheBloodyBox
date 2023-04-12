using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrosiveFloorHandler : MonoBehaviour
{
    float timer;

    void Update()
    {
        timer = timer + Time.deltaTime;

        if(timer > 3)
        {
            gameObject.SetActive(false);
        }
    }
}
