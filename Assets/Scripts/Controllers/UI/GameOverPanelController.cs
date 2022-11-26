using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Data.UnityObject;
using Data.ValueObject;

public class GameOverPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private GameObject successPanel, failPanel;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private GameObject[] crowns;
    [SerializeField] private Image[] stageNodes;
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderImage;

    [SerializeField] private int stageNum = 0, levelNum = 0;

    #endregion
    #region Private Variables
    private UIData _data;
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
    private UIData GetData() => Resources.Load<CD_UI>("Data/CD_UI").Data;
    private void Start()
    {
        InitializeStageNum();
    }

    private void InitializeStageNum()
    {
        stageNum = SaveSignals.Instance.onGetScore(SaveLoadStates.StageNum, SaveFiles.SaveFile);
        levelNum = SaveSignals.Instance.onGetScore(SaveLoadStates.Level, SaveFiles.SaveFile);
    }

    public void CloseGameOverPanel()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
    }

    public void MenuBtn()
    {
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);

        ClearTournamentPart();
    }

    private void ClearTournamentPart()
    {
        foreach (var i in stageNodes)
        {
            i.color = _data.DefaultStageColor;
        }
        foreach (var i in crowns)
        {
            i.SetActive(false);
        }
        sliderImage.color = _data.SuccessStageColor;

    }

    public void OnStageSuccessFul()
    {
        ++stageNum;
        TournamentPartSuccess();

        if (stageNum == stageNodes.Length - 1)
        {
            LevelSignals.Instance.onFinalStage?.Invoke();
        }
        else if (stageNum == stageNodes.Length)
        {
            LevelSignals.Instance.onTournamentWon?.Invoke();
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            SaveSignals.Instance.onSaveScore?.Invoke(++levelNum, SaveLoadStates.Level, SaveFiles.SaveFile);
            //level'i arttýrýp, Farklý bir panelden ödül seçtirmeli veya direk menuye atmalý
            stageNum = 0;
        }
        SaveSignals.Instance.onSaveScore?.Invoke(stageNum, SaveLoadStates.StageNum, SaveFiles.SaveFile);

    }

    private void TournamentPartSuccess()
    {
        for (int i = 0; i < stageNum; i++)
        {
            crowns[i].SetActive(true);
            if (i == stageNodes.Length)
            {
                return;
            }
            stageNodes[i].color = _data.SuccessStageColor;
        }
        slider.value = _data.SliderValues[stageNum];
        successPanel.SetActive(true);
        failPanel.SetActive(false);
    }

    public void OnStageFailed()
    {
        stageNum = 0;
        TournamentPartFail();
    }

    private void TournamentPartFail()
    {
        for (int i = 0; i < stageNodes.Length; i++)
        {
            stageNodes[i].color = _data.FailStageColor;
        }
        sliderImage.color = _data.FailStageColor;
        SaveSignals.Instance.onSaveScore?.Invoke(stageNum, SaveLoadStates.StageNum, SaveFiles.SaveFile);

        successPanel.SetActive(false);
        failPanel.SetActive(true);
    }
}
