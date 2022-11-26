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
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public bool IsTimeUp = false;

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables
        private PlayerData _data;
        private PlayerMovementController _movementController;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {

            _data = GetData();
            _movementController = GetComponent<PlayerMovementController>();
        }
        public PlayerData GetData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onClicked += _movementController.OnClicked;


            CoreGameSignals.Instance.onPlay += _movementController.OnPlay;
            CoreGameSignals.Instance.onPlayPressed += _movementController.OnPlayPressed;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onRestartLevel += _movementController.OnReset;
            CoreGameSignals.Instance.onRestartLevel += OnResetLevel;

        }

        private void UnsubscribeEvents()
        {

            InputSignals.Instance.onClicked -= _movementController.OnClicked;


            CoreGameSignals.Instance.onPlay -= _movementController.OnPlay;
            CoreGameSignals.Instance.onPlayPressed -= _movementController.OnPlayPressed;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onRestartLevel -= _movementController.OnReset;
            CoreGameSignals.Instance.onRestartLevel -= OnResetLevel;


        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnPlay()
        {
            IsTimeUp = false;
        }
        private void OnResetLevel()
        {
            IsTimeUp = false;
        }
    }
}