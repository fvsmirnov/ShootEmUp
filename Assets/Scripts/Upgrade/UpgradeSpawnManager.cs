using DigitalRuby.Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeSpawnManager : MonoBehaviour
{
    public static UpgradeSpawnManager Instance { get; private set; }

    public GameObject[] upgrades;
    public float minTimeSpawn = 2f;
    public float maxTimeSpawn = 6f;

    private List<UpgradeBase> activeUpgrades = new List<UpgradeBase>();
    private Vector2 bound;
    private Vector3 upgradeSpawnPos;
    private float spawnTime = 0;

    private void OnDisable()
    {
        Unload();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        bound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        upgradeSpawnPos = Vector3.up * (bound.y + 1f);  //Set y spawn coord
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while(true)
        {
            if(Time.time > spawnTime)
            {
                spawnTime = Time.time + Random.Range(minTimeSpawn, maxTimeSpawn);
                GameObject go;

                if (upgrades.Length > 1)
                {
                    string upgrade = upgrades[Random.Range(0, upgrades.Length)].name;
                    go = SpawningPool.CreateFromCache(upgrade);
                }
                else
                    go = SpawningPool.CreateFromCache(upgrades[0].name);

                //Set position
                upgradeSpawnPos.x = Random.Range(-bound.x, bound.x);
                go.transform.position = upgradeSpawnPos;

                //Subscribe
                var upgradeComponent = go.GetComponent<UpgradeBase>();
                activeUpgrades.Add(upgradeComponent);
            }

            yield return null;
        }
    }

    public void ResetData()
    {
        Unload();
        Load();
    }

    public void Unload()
    {
        StopCoroutine(Spawning());

        foreach (var upgrade in activeUpgrades)
            if (upgrade != null)
                upgrade.Destroy();

        activeUpgrades.Clear();
    }

    public void Load()
    {
        StartCoroutine(Spawning());
    }
}
