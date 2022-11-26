using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables



        #endregion
        #region Private Variables
        private Camera _cam;
        private LevelData _data;
        private int _levelId = 0;
        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            _levelId = GetLevelId();
            ChangeColor();
        }

        private int GetLevelId() => LevelSignals.Instance.onGetCurrentModdedLevel();

        private void Init()
        {
            _cam = GetComponent<Camera>();
            _data = GetData();
        }
        public LevelData GetData() => Resources.Load<CD_Level>("Data/CD_Level").Data;


        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;

            LevelSignals.Instance.onTournamentWon += OnTournamentWon;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;

            LevelSignals.Instance.onTournamentWon -= OnTournamentWon;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void ChangeColor()
        {
            _cam.backgroundColor = _data.LevelColors[_levelId];
            RenderSettings.fogColor = _data.LevelColors[_levelId];
        }

        private void OnPlay()
        {

        }
        private void OnTournamentWon()
        {
            
        }
        private void OnRestartLevel()
        {
            _levelId = GetLevelId();
            ChangeColor();
        }
    }
}