using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour
{
    [HideInInspector] public HealthSystem healthSystem;

    protected virtual void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDead += Destroy;
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
