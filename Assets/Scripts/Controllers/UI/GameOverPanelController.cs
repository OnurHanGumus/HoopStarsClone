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

    [SerializeField] private int stageNum = 0;

    #endregion
    #region Private Variables
    private int _highScore;
    private bool _isSuccess = false;
    private UIData _data;
    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _highScore = InitializeHighScore();
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
    }

    public void CloseGameOverPanel()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
    }
    private int InitializeHighScore()
    {
        return SaveSignals.Instance.onGetScore(SaveLoadStates.Score, SaveFiles.SaveFile);
    }
    //public void ShowThePanel()
    //{
    //    int temp = ScoreSignals.Instance.onGetScore();

    //    if(temp > _highScore)
    //    {
    //        successPanel.SetActive(true);
    //        failPanel.SetActive(false);
    //        scoreTxt.text = "High Score: " + temp;
    //        _highScore = temp;
    //        SaveSignals.Instance.onSaveScore?.Invoke(temp,SaveLoadStates.Score,SaveFiles.SaveFile);
    //    }
    //    else
    //    {
    //        successPanel.SetActive(false);
    //        failPanel.SetActive(true);
    //        scoreTxt.text = "Score: " + temp;
    //    }
    //}

    public void TryAgainBtn()
    {
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        CoreGameSignals.Instance.onPlay?.Invoke();
    }
    public void MenuBtn()
    {
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
    }

    [Button]
    public void Open()
    {
        CoreGameSignals.Instance.onStageFailed?.Invoke();
    }

    public void OnStageSuccessFul()
    {
        _isSuccess = true;
        TournamentPartSuccess();
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
        ++stageNum;

    }

    public void OnStageFailed()
    {
        _isSuccess = false;
    }

}
