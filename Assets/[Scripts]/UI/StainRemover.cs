using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainRemover : MonoBehaviour
{
    [Range(0f, 60f)]
    [SerializeField] float timer;
    float alpha = 0.9f;
    private bool doOnce = false;
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] GameObject bloodPrefab;

    float timeElapsed = 0;
   [SerializeField] float lerpDuration = 29;

    void Update()
    {

        
            if (SpawnManager.Instance.intermissionOn)
            {
                      timer = timer+ Time.deltaTime;
                       if (timer > 1.0f)
                       {
                           gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
                           alpha -= Time.deltaTime;
                           timer = 0;
                            lerpDuration--;
                            
                       }
                        else if(lerpDuration <= 0)
                        {

                        lerpDuration = 20;
                        alpha = 1;
                        this.gameObject.SetActive(false);
                         bloodPrefab.gameObject.SetActive(false );
                         gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
                         EnemyPrefab.GetComponent<EnemyBehaviour>().ReturnEnemy();
                
                        
                        }      
            }
            else
        {
            timeElapsed = 0;
        }
    }


}
