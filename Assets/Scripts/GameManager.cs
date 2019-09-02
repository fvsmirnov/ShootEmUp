using DigitalRuby.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int score = 0;

    private Vector2 screenBounds;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    private void Start()
    {
        UIScore.OnScoreChanged += UpdateScore;
    }

    private void UpdateScore(int newScore)
    {
        score = newScore;
    }

    public Vector2 ScreenBounds() => screenBounds;

    public void RestartGame()
    {
        StageManager.Instance.ResetData();
        UpgradeSpawnManager.Instance.ResetData();
        UIScore.Instance.ResetData();
        PlayerController.Instance.Spawn();

        SpawningPool.RecycleActiveObjects();
        StageManager.Instance.LoadStage();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
