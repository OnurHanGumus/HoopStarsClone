using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class TargetManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables
        private TargetData _data;
        private bool _isFinalStage = false;
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
        public TargetData GetData() => Resources.Load<CD_Target>("Data/CD_Target").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onFinalStage += OnFinalStage;
            LevelSignals.Instance.onTimeUp += OnTimeUp;
            LevelSignals.Instance.onBasket += OnBasket;

            CoreGameSignals.Instance.onRestartLevel += OnResetLevel;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onFinalStage -= OnFinalStage;
            LevelSignals.Instance.onTimeUp -= OnTimeUp;
            LevelSignals.Instance.onBasket -= OnBasket;

            CoreGameSignals.Instance.onRestartLevel -= OnResetLevel;
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnPlay()
        {
            
        }
        private void OnResetLevel()
        {
            transform.position = _data.BallPositions[0];
        }

        private void OnTimeUp()
        {
            _isFinalStage = false;

        }

        private void OnFinalStage()
        {
            _isFinalStage = true;
        }

        private void OnBasket()
        {
            if (_isFinalStage)
            {
                transform.position = _data.BallPositions[Random.Range(0, _data.BallPositions.Length - 1)];
            }
        }
    }
}