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
    [SerializeField] private TextMeshProUGUI goText;
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
    
    public void OnPlayPressed()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(_data.StartDelay);
        goText.DOFade(1, 0.2f);
        CoreGameSignals.Instance.onPlay?.Invoke();
        yield return new WaitForSeconds(0.4f);
        goText.DOFade(0, 0.2f);
    }

}
