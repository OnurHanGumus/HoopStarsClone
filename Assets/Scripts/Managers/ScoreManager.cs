using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Extentions;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Enums;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables


        #endregion

        #region Private Variables

        private int _playerScore;
        private int _enemyScore;
        [ShowInInspector]
        public int PlayerScore
        {
            get { return _playerScore; }
            set { _playerScore = value; }
        }
        [ShowInInspector]

        public int EnemyScore
        {
            get { return _enemyScore; }
            set { _enemyScore = value; }
        }



        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {

        }
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease += OnScoreIncrease;
            ScoreSignals.Instance.onScoreDecrease += OnScoreDecrease;
            ScoreSignals.Instance.onGetScore += OnGetScore;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            LevelSignals.Instance.onTimeUp += OnTimeUp;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onScoreIncrease -= OnScoreIncrease;
            ScoreSignals.Instance.onScoreDecrease -= OnScoreDecrease;
            ScoreSignals.Instance.onGetScore -= OnGetScore;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
            LevelSignals.Instance.onTimeUp -= OnTimeUp;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnScoreIncrease(ScoreTypeEnums type, int amount)
        {
            if (type.Equals(ScoreTypeEnums.Score))
            {
                PlayerScore += amount;
                UISignals.Instance.onSetChangedText?.Invoke(type, PlayerScore);
            }
            else if (type.Equals(ScoreTypeEnums.EnemyScore))
            {
                EnemyScore += amount;
                UISignals.Instance.onSetChangedText?.Invoke(type, EnemyScore);
            }

        }

        private void OnScoreDecrease(ScoreTypeEnums type, int amount)
        {

        }


        private int OnGetScore()
        {
            return PlayerScore;
        }

        private void OnRestartLevel()
        {
            PlayerScore = 0;
            EnemyScore = 0;
        }

        private void OnTimeUp()
        {
            if (PlayerScore >= EnemyScore)
            {
                CoreGameSignals.Instance.onStageSuccessful?.Invoke();

            }
            else
            {
                CoreGameSignals.Instance.onStageFailed?.Invoke();
            }
        }
    }
}