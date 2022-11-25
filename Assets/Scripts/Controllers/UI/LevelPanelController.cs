using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshPro scoreText, timerText, enemyScoreText;
    [SerializeField] private int time = 60, currentTime = 60;
    #endregion
    #region Private Variables
    private LevelData _data;
    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _data = GetData();

    }
    private LevelData GetData() => Resources.Load<CD_Level>("Data/CD_Level").Data;
    private IEnumerator Timer()
    {
        --currentTime;
        if (currentTime.ToString().Length == 1)
        {
            timerText.text = "00:0" + currentTime;
        }
        else
        {
            timerText.text = "00:" + currentTime;
        }
        yield return new WaitForSeconds(1f);

        if (currentTime <= 0)
        {
            LevelSignals.Instance.onTimeUp?.Invoke();
        }
        else
        {
            StartCoroutine(Timer());
        }
    }

    public void OnScoreUpdateText(ScoreTypeEnums type, int score)
    {
        if (type.Equals(ScoreTypeEnums.Score))
        {
            scoreText.text = score.ToString();
        }
        else if (type.Equals(ScoreTypeEnums.EnemyScore))
        {
            enemyScoreText.text = score.ToString();
        }
    }

    public void OnRestartLevel()
    {
        scoreText.text = 0.ToString();
        
    }

    public void OnPlay()
    {
        currentTime = _data.TimerCount;
        StartCoroutine(Timer());
    }

   
}
