using DigitalRuby.Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class Stage : MonoBehaviour, IStage
{
    public event Action OnStageComplete = delegate { };
    public Path path;
    public GameObject enemyPrefab;
    public int enemyCount = 5;
    public float spawnEnemyDelay = 1f;

    private List<EnemyShip> ships = new List<EnemyShip>();
    private readonly Vector3 spawnPosition = Vector3.down * 10;
    private int currentActiveShipCount;
    private int spawnedShipsCount;
    private float lastSpawnTime;

    IEnumerator SpawnWithDelay(int amount, float delay)
    {
        while (ships.Count < enemyCount)
        {
            if (Time.time >= lastSpawnTime)
            {
                lastSpawnTime = Time.time + spawnEnemyDelay;

                GameObject go = SpawningPool.CreateFromCache(enemyPrefab.name);
                go.transform.position = spawnPosition;  //For safe object spawn   

                EnemyShip ship = go.GetComponent<EnemyShip>();
                ship.OnDeactivate += RecountActiveShips;
                ship.SetPath(path);
                ships.Add(ship);

                currentActiveShipCount++;
                spawnedShipsCount++;
            }
            yield return null;
        }
    }

    //Decrease active ships.
    //Called when subscibed ship destroyed
    void RecountActiveShips(object sender, EventArgs e)
    {
        var ship = (EnemyShip)sender;
        ship.OnDeactivate -= RecountActiveShips;

        currentActiveShipCount--;

        if (!reset)                                                               //To avoid unnecessary event call
            if (currentActiveShipCount == 0 && spawnedShipsCount == enemyCount)   //Check if all ships already were spawned
                OnStageComplete();
    }

    //Set default values
    bool reset = false;
    public void ResetData()
    {
        StopCoroutine(SpawnWithDelay(enemyCount, enemyCount));

        reset = true;
        foreach(var ship in ships)
            ship.Deactivate();

        ships.Clear();
        reset = false;

        spawnedShipsCount = 0;
        lastSpawnTime = 0f;
    }

    public void Execute()
    {
        StartCoroutine(SpawnWithDelay(enemyCount, enemyCount));
    }
}
