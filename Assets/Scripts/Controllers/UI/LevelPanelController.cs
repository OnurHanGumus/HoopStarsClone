using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Data.UnityObject;
using DG.Tweening;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshPro scoreText, timerText;
    [SerializeField] private int time = 60, currentTime = 60;
    #endregion
    #region Private Variables

    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {


    }

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
    }

    public void OnRestartLevel()
    {
        scoreText.text = 0.ToString();
    }

    public void OnPlay()
    {
        StartCoroutine(Timer());
    }

   
}
