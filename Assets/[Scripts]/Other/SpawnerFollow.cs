using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFollow : MonoBehaviour
{
    public PlayerBehaviour target;
    private void Update()
    {
        Vector3 pos =  transform.position;
        pos = target.transform.position;
        transform.position = pos;

       
    }
}
