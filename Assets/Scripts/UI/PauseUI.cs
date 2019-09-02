using DigitalRuby.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance { get; private set; }
    public GameObject pauseWindow;
    public Text score;

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
        score.text = GameManager.Instance.score.ToString();
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
        GameManager.Instance.RestartGame();
    }
}
