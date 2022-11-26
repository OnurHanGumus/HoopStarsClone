using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelActivenessController uiPanelController;
        [SerializeField] private GameOverPanelController gameOverPanelController;
        [SerializeField] private LevelPanelController levelPanelController;
        [SerializeField] private HighScorePanelController highScorePanelController;
        [SerializeField] private StartPanelController startPanelController;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetChangedText += levelPanelController.OnScoreUpdate;
            UISignals.Instance.onSetChangedText += startPanelController.OnScoreUpdate;
            CoreGameSignals.Instance.onPlayPressed += OnPlayPressed;
            CoreGameSignals.Instance.onPlayPressed += levelPanelController.OnPlayPressed; ;
            CoreGameSignals.Instance.onPlay += levelPanelController.OnPlay;
            CoreGameSignals.Instance.onPlay += startPanelController.OnPlay;
            CoreGameSignals.Instance.onStageFailed += OnStageFailed;
            CoreGameSignals.Instance.onStageFailed += gameOverPanelController.OnStageFailed;
            CoreGameSignals.Instance.onStageSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onStageSuccessful += gameOverPanelController.OnStageSuccessFul;
            CoreGameSignals.Instance.onRestartLevel += levelPanelController.OnRestartLevel;
            CoreGameSignals.Instance.onRestartLevel += startPanelController.OnRestartLevel;
            ScoreSignals.Instance.onHighScoreChanged += highScorePanelController.OnUpdateText;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetChangedText -= levelPanelController.OnScoreUpdate;
            UISignals.Instance.onSetChangedText -= startPanelController.OnScoreUpdate;
            CoreGameSignals.Instance.onPlayPressed -= OnPlayPressed;
            CoreGameSignals.Instance.onPlayPressed += levelPanelController.OnPlayPressed; ;
            CoreGameSignals.Instance.onPlay -= levelPanelController.OnPlay;
            CoreGameSignals.Instance.onPlay -= startPanelController.OnPlay;
            CoreGameSignals.Instance.onStageFailed -= OnStageFailed;
            CoreGameSignals.Instance.onStageFailed -= gameOverPanelController.OnStageFailed;
            CoreGameSignals.Instance.onStageSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onStageSuccessful -= gameOverPanelController.OnStageSuccessFul;
            CoreGameSignals.Instance.onRestartLevel -= levelPanelController.OnRestartLevel;
            CoreGameSignals.Instance.onRestartLevel -= startPanelController.OnRestartLevel;
            ScoreSignals.Instance.onHighScoreChanged -= highScorePanelController.OnUpdateText;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenMenu(panelParam);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.CloseMenu(panelParam);
        }

        private void OnPlayPressed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.GemPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnStageFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.GameOverPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.GemPanel);
            //gameOverPanelController.ShowThePanel();
        }

        private void OnLevelSuccessful()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.GameOverPanel);
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlayPressed?.Invoke();
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.WinPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void RestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void PauseButton()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PausePanel);
            Time.timeScale = 0f;
        }
        public void HighScoreButton()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.HighScorePanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
        }
        public void OptionsButton()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.OptionsPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
        }
    }
}