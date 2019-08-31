using DigitalRuby.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance { get; private set; }
    public GameObject pauseWindow;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        var player = FindObjectOfType<PlayerShip>();
        player.OnDestroyed += ShowPauseWindow;
    }

    public void ShowPauseWindow()
    {
        Time.timeScale = 0;
        pauseWindow.SetActive(true);
    }

    public void HidePauseWindow()
    {
        pauseWindow.SetActive(false);
        Time.timeScale = 1;
    }

    public void TryAgain()
    {
        HidePauseWindow();
        StageManager.Instance.ResetData();
        UpgradeSpawnManager.Instance.ResetData();
        UIScore.Instance.ResetData();
        PlayerController.Instance.Spawn();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
