﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public struct ScreenBounds
{
    public Boundary horizontal;
    public Boundary vertical;
}



public class BulletBehaviour: MonoBehaviour
{
    [Header("Bullet Properties")]
    public ScreenBounds bounds;
    public BulletType bulletType;
    private BulletManager bulletManager;


    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
    }

    private void FixedUpdate()
    {
        CheckBounds();
    }

    void CheckBounds()
    {
        if ((transform.position.x > bounds.horizontal.max) ||
            (transform.position.x < bounds.horizontal.min) ||
            (transform.position.y > bounds.vertical.max) ||
            (transform.position.y < bounds.vertical.min))
        {
            bulletManager.ReturnBullet(this.gameObject, bulletType);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //if ((bulletType == BulletType.PLAYER) ||
        //    (bulletType == BulletType.ENEMY && other.gameObject.CompareTag("Player")))
        //{
        //    bulletManager.ReturnBullet(this.gameObject, bulletType);
        //}
        
    }

}