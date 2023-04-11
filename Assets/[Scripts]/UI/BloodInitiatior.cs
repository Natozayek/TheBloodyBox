using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodInitiatior : MonoBehaviour
{
    [SerializeField] ParticleSystem ParticleSystem;
    [SerializeField] float durationTime;

    float timer;
    
    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;

        if(timer > Random.Range(1.5f,durationTime))
        {
            ParticleSystem.Play();
            timer = 0;
        }
        
    }
}
