using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public Stage[] stages;
    [Space]
    public int startStage;

    [Header("Loop settings")]
    public bool loop;
    [Tooltip("Replace enemy prefab after complete one cycle")]
    public GameObject[] loopEnemys;

    private int currentStageIndex;
    private int completeLoopCount;
    private Stage currentStage;

    public void ResetData()
    {
        //Init stage
        currentStageIndex = (startStage != 0 && stages[startStage] != null) ? startStage : 0;
        SetStage(currentStageIndex);

        //Init loop data
        completeLoopCount = 0;
        if (loop && loopEnemys.Length > 0)
            currentStage.enemyPrefab = loopEnemys[0];
    }

    public void LoadStage()
    {
        if (!loop && currentStageIndex == stages.Length - 1)    //Return if it last stage and without loop
            return;

        SetStage(currentStageIndex);
        SetEnemyPrefab();

        currentStage.gameObject.SetActive(true);
        currentStage.Execute();
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
        LoadStage();
    }

    //Replace current stage by one of the list.
    private void SetStage(int stageIndex)
    {
        if (currentStage != null)
        {
            currentStage.OnStageComplete -= NextStage;
            currentStage.gameObject.SetActive(false);
        }
        currentStage = stages[stageIndex];
        currentStage.OnStageComplete += NextStage;
        currentStage.ResetData();
    }

    private void NextStage()
    {
        ChangeStageIndex();
        LoadStage();
    }

    private void ChangeStageIndex()
    {
        currentStageIndex++;
        if (currentStageIndex > stages.Length - 1)
        {
            if (loop)
            {
                RecalculateCompleteLoopCount();
                currentStageIndex = 0;
            }
            else
                currentStageIndex = stages.Length - 1;
        }
    }

    private void RecalculateCompleteLoopCount()
    {
        if (loop)
        {
            completeLoopCount++;
            completeLoopCount = Mathf.Clamp(completeLoopCount, 0, loopEnemys.Length - 1);
        }
    }

    private void SetEnemyPrefab()
    {
        if (loop && loopEnemys.Length > 0)
            currentStage.enemyPrefab = loopEnemys[completeLoopCount];
    }
}
