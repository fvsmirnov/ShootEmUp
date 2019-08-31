using DigitalRuby.Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeBase : MonoBehaviour, IPooledObject
{
    protected int lifeTime = 5;
    private float timeToEnd = 0;

    public virtual void Destroy()
    {
        StopCoroutine(WaitBeforeDestroy());
        SpawningPool.ReturnToCache(gameObject);
    }

    protected virtual void OnEnable()
    {
        TimeDelay();
        StartCoroutine(WaitBeforeDestroy());
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Execute(collision);
            Destroy();
        }
    }

    protected virtual void Execute(Collider2D collision) { }

    protected virtual void TimeDelay()
    {
        timeToEnd = Time.time + lifeTime;
    }

    IEnumerator WaitBeforeDestroy()
    {
        while (Time.time < timeToEnd)
            yield return null;

        Destroy();
    }

    #region Pool data
    public virtual void PooledObjectInstantiated() { }

    public virtual void PooledObjectSpawned() { }

    public virtual void PooledObjectReturnedToPool() { }
    #endregion
}
