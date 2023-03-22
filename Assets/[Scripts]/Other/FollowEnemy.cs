using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public EnemyBehaviour enemyChild;
    public Quaternion enemyRotation;

    private void Update()
    {
        transform.position = enemyChild.transform.position;
        enemyRotation = enemyChild.transform.rotation;
        enemyRotation.z = enemyRotation.z * -1;
    }
}
