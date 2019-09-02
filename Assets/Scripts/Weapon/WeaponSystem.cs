using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject bulletPrefab;

    private IWeapon[] weapons;
    private IWeapon currentWeapon;
    private IEnumerator upgradeWorkTime;

    public void Shoot()
    {
        currentWeapon.Shoot();
    }

    /// <summary>
    /// Set bullet prefab to all weapons
    /// </summary>
    public void SetBullet(GameObject bulletPrefab)
    {
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].Bullet = bulletPrefab;
    }

    /// <summary>
    /// Set weapon for some time
    /// </summary>
    /// <param name="duration">Ignore if is unlimited true</param>
    public void SetWeapon(int weaponId, float duration = 15f, bool isUnlimited = false, GameObject bulletPrefab = null) //set private
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

                upgradeWorkTime = UpgradeWorkTime(Time.time + duration);
                StartCoroutine(upgradeWorkTime);
            }
        }   
    }

    public void SetDefaultWeapon()
    {
        SetWeapon(0, isUnlimited: true, bulletPrefab: bulletPrefab);
    }


    private void Start()
    {
        weapons = GetComponentsInChildren<IWeapon>();
        SetBullet(bulletPrefab);
        SetDefaultWeapon();
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
