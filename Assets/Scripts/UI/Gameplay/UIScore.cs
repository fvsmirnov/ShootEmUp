using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    public static Action<int> OnScoreChanged = delegate { };
    public static UIScore Instance { get; private set; }
    public Text text;

    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateScore(int value)
    {
        score += value;
        text.text = score.ToString();
        OnScoreChanged(score);
    }

    public void ResetData()
    {
        score = 0;
        text.text = score.ToString();
    }
}
