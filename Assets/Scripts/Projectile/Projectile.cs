using DigitalRuby.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{
    [Header("Target")]
    public bool enemy;
    public bool player;

    public int damageAmount = 1;
    public float lifeTime = 6f;

    private float lifeTimeEnd = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy == true && collision.tag == "Enemy")
        {
            SetDamage(collision);
            Destroy();
        }

        if (player == true && collision.tag == "Player")
        {
            SetDamage(collision);
            Destroy();
        }
    }

    //Apply damage when collides with ship
    private void SetDamage(Collider2D collision)
    {
        var ship = collision.GetComponent<ShipBehaviour>();
        if (ship != null)
            ship.healthSystem.Damage(damageAmount);
    }

    private void Destroy()
    {
        SpawningPool.ReturnToCache(gameObject);
    }

    private IEnumerator Wait()
    {
        while (Time.time < lifeTimeEnd)
        {
            yield return null;
        }
        Destroy();
    }

    void ResetData()
    {
        lifeTimeEnd = Time.time + lifeTime;
        StartCoroutine(Wait());
    }

    #region Pool Data
    public void PooledObjectReturnedToPool()
    {
        StopCoroutine(Wait());
    }

    public void PooledObjectInstantiated() { ResetData(); }

    public void PooledObjectSpawned()
    {
        gameObject.SetActive(true);
        ResetData();
    }
    #endregion
}
