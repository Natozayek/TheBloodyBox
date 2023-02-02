using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DeathPlaneController : MonoBehaviour
{
    public Transform currentCheckPoint;
    private SoundManager soundManager;

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
           

            //other.gameObject.GetComponent<PlayerBehaviour>().life.LoseLife();
            //other.gameObject.GetComponent<PlayerBehaviour>().health.resetHeath();

            //if (other.gameObject.GetComponent<PlayerBehaviour>().life.value > 0)
            {
                ReSpawn(other.gameObject);

                soundManager.PlaySoundFX(SoundFX.DEATH, SoundChannel.PLAYER_DEATH_FX);

            }


            
        }
    }

    public void ReSpawn(GameObject go)
    {
        Debug.Log("here");
        go.transform.position = currentCheckPoint.position;
    }
}
