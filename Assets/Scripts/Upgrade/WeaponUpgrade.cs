using DigitalRuby.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : UpgradeBase
{
    private Transform _transform;
    private readonly Vector3 moveDirection = Vector3.down;

    [SerializeField] private int speed = 3;
    [SerializeField] private int weaponId = 0;
    [SerializeField] private float duration = 15f;

    private void Awake()
    {
        _transform = gameObject.transform;
    }

    protected override void Execute(Collider2D collision)
    {
        WeaponSystem weaponSystem = collision.GetComponent<WeaponSystem>();
        weaponSystem.SetWeapon(weaponId, duration);
    }

    protected void LateUpdate()
    {
        _transform.position += moveDirection * speed * Time.deltaTime;
    }
}
