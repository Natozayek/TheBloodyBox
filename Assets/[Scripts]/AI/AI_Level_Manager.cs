using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Level_Manager : MonoBehaviour
{
    public int numberOfMembers;
    public int numOfEnemies;
    public List<EnemyBehaviour> ListOfEnemies;
    public List<BulletBehaviour> ListOfBullets;
    public float bounds;
    public float spawnRadius;
    void Awake()
    {
        ListOfEnemies = new List<EnemyBehaviour>();
        ListOfBullets = new List<BulletBehaviour>();
       

    }
    public List<EnemyBehaviour> GetEnemyNeightbourhs(EnemyBehaviour memberX, float radius)
    {
        List<EnemyBehaviour> neiboursFound = new List<EnemyBehaviour>();
        foreach (var other in ListOfEnemies)
        {
            if (other == memberX)
            {
                continue;
            }

            if (Vector3.Distance(memberX.transform.position, other.transform.position) <= radius)
            {
                neiboursFound.Add(other);
            }
        }
        return neiboursFound;
    }

    public List<BulletBehaviour> GetBullets(BulletBehaviour memberBullet, float Radius)
    {
        List<BulletBehaviour> returnBullets = new List<BulletBehaviour>();

        foreach (var other in ListOfBullets)
        {

            if (Vector3.Distance(memberBullet.transform.position, other.transform.position) <= Radius)
            {
                returnBullets.Add(other);
            }
        }

        return returnBullets;

    }
}
