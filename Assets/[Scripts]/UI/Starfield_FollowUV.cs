using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield_FollowUV : MonoBehaviour
{
    private PlayerBehaviour playerRef;
    private void Start()
    {
        playerRef = FindObjectOfType<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( new Vector3(playerRef.movementDirection.x * -playerRef.movementSpeed,
                                                          playerRef.movementDirection.y * -playerRef.movementSpeed, 0) * Time.deltaTime );
    }
}
