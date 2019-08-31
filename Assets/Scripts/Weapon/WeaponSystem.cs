using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject bulletPrefab;

    private IWeapon[] weapons;
    private IWeapon currentWeapon;
    private IEnumerator upgradeWorkTime;

    private void Start()
    {
        weapons = GetComponentsInChildren<IWeapon>();
        SetBullet(bulletPrefab);
        SetDefaultWeapon();
    }

    public void Shoot()
    {
        currentWeapon.Shoot();
    }

    public void SetBullet(GameObject bulletPrefab)
    {
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].Bullet = bulletPrefab;
    }

    public void SetWeapon(int weaponId, float time = 15f, bool isUnlimited = false, GameObject bulletPrefab = null) //set private
    {
        if(weapons[weaponId] != null)
        {
            currentWeapon = weapons[weaponId];

            if (bulletPrefab != null)
                SetBullet(bulletPrefab);

            //Reset coroutine
            if(!isUnlimited)
            {
                if(upgradeWorkTime != null)
                    StopCoroutine(upgradeWorkTime);

                upgradeWorkTime = UpgradeWorkTime(Time.time + time);
                StartCoroutine(upgradeWorkTime);
            }
        }   
    }

    public void SetDefaultWeapon()
    {
        SetWeapon(0, isUnlimited: true, bulletPrefab: bulletPrefab);
    }

    private IEnumerator UpgradeWorkTime(float time)
    {
        while (Time.time < time)
        {
            yield return null;
        }

        SetDefaultWeapon();
    }
}
