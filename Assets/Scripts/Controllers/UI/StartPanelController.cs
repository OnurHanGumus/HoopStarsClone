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

public class StartPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI tournamentText, stageText, gemText;
    #endregion
    #region Private Variables
    private LevelData _data;
    private int _levelId, _stageId;
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
    private void Start()
    {
        GetValues();
        UpdateTexts();
    }
    private void GetValues()
    {
        _levelId = GetLevelId();
        _stageId = GetStageId();
    }
    private LevelData GetData() => Resources.Load<CD_Level>("Data/CD_Level").Data;

    private int GetLevelId() => SaveSignals.Instance.onGetScore(SaveLoadStates.Level, SaveFiles.SaveFile);
    private int GetStageId() => SaveSignals.Instance.onGetScore(SaveLoadStates.StageNum, SaveFiles.SaveFile);

    private void UpdateTexts()
    {
        tournamentText.text = "TOURNAMENT " + (_levelId + 1);
        stageText.text = ((StageTextsEnum)_stageId).ToString();
    }

    public void OnRestartLevel()
    {
        GetValues();
        UpdateTexts();
    }

    public void OnPlay()
    {
        //UpdateTexts();
    }

    public void OnScoreUpdate(ScoreTypeEnums type, int newValue)
    {
        if (type.Equals(ScoreTypeEnums.Gem))
        {
            gemText.text = newValue.ToString();
        }
    }
}
