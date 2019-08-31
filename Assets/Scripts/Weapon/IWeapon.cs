using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    GameObject Bullet { get; set; }
    void Shoot();
}
