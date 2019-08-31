using DigitalRuby.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    public Transform[] barrels;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int fireDelay = 1;
    private float nextFireTime = 0;

    public GameObject Bullet {
        get { return bulletPrefab; }
        set { bulletPrefab = value; }
    }

    public void Shoot()
    {
        if(Time.time >= nextFireTime)
        {
            for (int i = 0; i < barrels.Length; i++)
            {
                GameObject go = SpawningPool.CreateFromCache(bulletPrefab.name);
                go.transform.position = barrels[i].position;
                go.transform.rotation = Quaternion.identity;
            }
            nextFireTime = Time.time + fireDelay;
        } 
    }
}
