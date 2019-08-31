using DigitalRuby.Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : ShipBehaviour, IPooledObject
{
    public event EventHandler OnDeactivate = delegate { };
    public IWeapon weapon;

    [SerializeField] private float minShootDelay = 3f;
    [SerializeField] private float maxShootDelay = 6f;

    private float shootTime = 0;
    private PathFollow pathFollow;

    private void Awake()
    {
        pathFollow = GetComponent<PathFollow>();
        weapon = GetComponent<IWeapon>();
    }

    protected override void Start()
    {
        base.Start();
        shootTime = SetShootDelay();

        if (pathFollow != null)
            pathFollow.OnMoveComplete += Deactivate;
    }

    private void Update()
    {
        if (Time.time >= shootTime)
        {
            weapon.Shoot();
            shootTime += UnityEngine.Random.Range(minShootDelay, maxShootDelay);
        }
    }

    private float SetShootDelay()
    {
        return Time.time + UnityEngine.Random.Range(minShootDelay, maxShootDelay);
    }

    #region Pool Data
    public void PooledObjectInstantiated() { }

    public void PooledObjectSpawned()
    {
        if(gameObject != null)
        {
            gameObject.SetActive(true);
            healthSystem.ResetData();
            shootTime = SetShootDelay();
        }
    }

    public void PooledObjectReturnedToPool() { }
    #endregion

    public void SetPath(Path path)
    {
        pathFollow.SetPath(path);
    }

    public override void Destroy()
    {
        UIScore.Instance.UpdateScore(100);
        Deactivate();
    }

    public void Deactivate()
    {
        OnDeactivate(this, EventArgs.Empty);
        SpawningPool.ReturnToCache(gameObject);
    }
}
