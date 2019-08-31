using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public GameObject playerPrefab;
    public Transform spawnPos;
    public PlayerHealthUI healthBar;
    private GameObject player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Spawn();
    }

    private void Start()
    {
        var health = player.GetComponent<HealthSystem>();
        healthBar.Init(health);
    }

    public void Spawn()
    {
        if (player == null)
            player = Instantiate(playerPrefab, spawnPos);
        else
        {
            player.SetActive(true);
            var playerBehaviour = player.GetComponent<PlayerShip>();
            playerBehaviour.ResetData();
            player.transform.position = spawnPos.position;
        }
    }
}
