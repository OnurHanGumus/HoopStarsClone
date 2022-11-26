using System;
using System.Collections.Generic;
using Commands;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Enums;
using Data.ValueObject;
using TMPro;
using System.Collections;
using Data.UnityObject;
using DG.Tweening;

namespace Managers
{
    public class GameBackgroundPanelManager : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        #endregion
        #region SerializeField Variables
        [SerializeField] private TextMeshPro scoreText, timerText, enemyScoreText;
        [SerializeField] private List<SpriteRenderer> playerScorePoints, enemyScorePoints;
        #endregion
        #region Private Variables
        private LevelData _data;
        private int _levelId;
        private int _currentTime;

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
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onSetChangedText += OnScoreUpdate;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            CoreGameSignals.Instance.onPlayPressed += OnPlayPressed;

        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onSetChangedText -= OnScoreUpdate;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
            CoreGameSignals.Instance.onPlayPressed -= OnPlayPressed;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private LevelData GetData() => Resources.Load<CD_Level>("Data/CD_Level").Data;

        private void MovePanel(float yPos)
        {
            transform.DOMoveY(yPos, _data.BackgroundPanelMoveDelay).SetEase(Ease.InOutBack);
        }
        private IEnumerator Timer()
        {
            --_currentTime;
            if (_currentTime.ToString().Length == 1)
            {
                timerText.text = "00:0" + _currentTime;
            }
            else
            {
                timerText.text = "00:" + _currentTime;
            }
            yield return new WaitForSeconds(1f);

            if (_currentTime <= 0)
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

                playerScorePoints[i].color = new Color32(255, 255, 255, (byte)_data.ScorePointsDefaultAlpha);
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
            MovePanel(_data.BackgroundPanelInitializePosY);
            scoreText.text = 0.ToString();
            enemyScoreText.text = 0.ToString();
            DeactivateScorePoints();
        }
        public void OnPlay()
        {
            _currentTime = _data.TimerCount;
            StartCoroutine(Timer());
            InitializeScorePoints();
        }

        private void OnPlayPressed()
        {
            MovePanel(_data.BackgroundPanelStartPosY);
        }

    }
}