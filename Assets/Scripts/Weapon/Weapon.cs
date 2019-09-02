using DigitalRuby.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    public Transform[] barrels;

    [Space]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireDelay = 1;
    [SerializeField] private int projectileAmount = 1;
    private float nextFireTime = 0;

    public GameObject Bullet
    {
        get { return bulletPrefab; }
        set { bulletPrefab = value; }
    }

    public void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            if (projectileAmount > 1)
                StartCoroutine(ProjectilesSpawn());
            else
                ProjectileSpawn();
        }
    }

    private void ProjectileSpawn()
    {
        for (int i = 0; i < barrels.Length; i++)
        {
            GameObject go = SpawningPool.CreateFromCache(bulletPrefab.name);
            go.transform.position = barrels[i].position;
            go.transform.rotation = Quaternion.identity;
        }
        nextFireTime = Time.time + fireDelay;
    }

    private IEnumerator ProjectilesSpawn()
    {
        int projectilesSpawned = 0;
        while (projectilesSpawned < projectileAmount)
        {
            ProjectileSpawn();
            projectilesSpawned++;
            yield return new WaitForSeconds(0.2f); //spawn delay multiple projectiles
        }
    }

    private void OnDisable()
    {
        StopCoroutine(ProjectilesSpawn());
    }
}
