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

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public bool IsTimeUp = false;
        public Transform Target;

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables
        private EnemyData _data;
        private EnemyMovementController _movementController;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _movementController = GetComponent<EnemyMovementController>();
            _data = GetData();
        }
        public EnemyData GetData() => Resources.Load<CD_Enemy>("Data/CD_Enemy").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += _movementController.OnPlay;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onRestartLevel += _movementController.OnReset;
            CoreGameSignals.Instance.onRestartLevel += OnResetLevel;

            LevelSignals.Instance.onBasket += OnBasket;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= _movementController.OnPlay;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onRestartLevel -= _movementController.OnReset;
            CoreGameSignals.Instance.onRestartLevel -= OnResetLevel;

            LevelSignals.Instance.onBasket -= OnBasket;
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void GetTargetTransform()
        {
            Target = LevelSignals.Instance.onGetTransform();
        }
        private void OnPlay()
        {
            IsTimeUp = false;
            GetTargetTransform();
        }

        private void OnTimeUp()
        {
            IsTimeUp = true;
        }

        private void OnBasket()
        {
            GetTargetTransform();
        }

        private void OnResetLevel()
        {
            IsTimeUp = false;
        }
    }
}