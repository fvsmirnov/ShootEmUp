using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject defaultBulletPrefab;

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
            else
                SetBullet(defaultBulletPrefab);

            //Reset coroutine
            if (!isUnlimited)
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
        SetWeapon(0, isUnlimited: true, bulletPrefab: defaultBulletPrefab);
    }


    private void Start()
    {
        weapons = GetComponentsInChildren<IWeapon>();
        SetBullet(defaultBulletPrefab);
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
