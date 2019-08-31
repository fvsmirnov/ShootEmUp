using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : ShipBehaviour
{
    public event Action OnDestroyed = delegate { };
    [HideInInspector] public WeaponSystem weapon;

    protected override void Start()
    {
        base.Start();
        weapon = GetComponent<WeaponSystem>();
    }

    protected void Update()
    {
        weapon.Shoot();
    }

    public override void Destroy()
    {
        OnDestroyed();
        gameObject.SetActive(false);
    }

    public void ResetData()
    {
        healthSystem.ResetData();
        weapon.SetDefaultWeapon();
    }
}
