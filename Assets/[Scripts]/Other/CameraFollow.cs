 using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerBehaviour target;
    private void Update()
    {
        Vector3 pos =  transform.position;
        pos = target.transform.position;
        pos.z = -10;
        transform.position = pos;

       
    }
}
