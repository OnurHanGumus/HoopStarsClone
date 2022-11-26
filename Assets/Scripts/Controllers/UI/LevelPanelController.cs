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
    [SerializeField] private int currentTime;
    [SerializeField] private List<SpriteRenderer> playerScorePoints, enemyScorePoints;
    #endregion
    #region Private Variables
    private LevelData _data;
    private int _levelId;
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

    private void InitializeScorePoints()
    {
        _levelId = LevelSignals.Instance.onGetCurrentModdedLevel();
        for (int i = 0; i < _data.BasketCountToWin[_levelId]; i++)
        {
            playerScorePoints[i].gameObject.SetActive(true);
            enemyScorePoints[i].gameObject.SetActive(true);
        }
    }

    private void DeactivateScorePoints()
    {
        for (int i = 0; i < playerScorePoints.Count; i++)
        {
            playerScorePoints[i].gameObject.SetActive(false);
            enemyScorePoints[i].gameObject.SetActive(false);

            playerScorePoints[i].color = new Color32(255, 255, 255, (byte) _data.ScorePointsDefaultAlpha);
            enemyScorePoints[i].color = new Color32(255, 255, 255, (byte)_data.ScorePointsDefaultAlpha);
        }
    }

    public void OnScoreUpdate(ScoreTypeEnums type, int score)
    {
        if (type.Equals(ScoreTypeEnums.Score))
        {
            scoreText.text = score.ToString();
            playerScorePoints[score - 1].color = new Color32(255, 255, 255, (byte)_data.ScorePointsIncreasedAlpha);
            if (score == _data.BasketCountToWin[_levelId])
            {
                LevelSignals.Instance.onTimeUp?.Invoke();
                StopAllCoroutines();
            }
        }
        else if (type.Equals(ScoreTypeEnums.EnemyScore))
        {
            enemyScoreText.text = score.ToString();
            enemyScorePoints[score - 1].color = new Color32(255, 255, 255, (byte)_data.ScorePointsIncreasedAlpha);
            if (score == _data.BasketCountToWin[_levelId])
            {
                LevelSignals.Instance.onTimeUp?.Invoke();
                StopAllCoroutines();
            }
        }
    }

    public void OnRestartLevel()
    {
        scoreText.text = 0.ToString();
        enemyScoreText.text = 0.ToString();
        DeactivateScorePoints();
    }

    public void OnPlay()
    {
        currentTime = _data.TimerCount;
        StartCoroutine(Timer());
        InitializeScorePoints();
    }

}
