using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelData
    {
        public int TimerCount = 59;
        public int[] BasketCountToWin;

        public float ScorePointsDefaultAlpha = 50f, ScorePointsIncreasedAlpha = 255f;
        public float StartDelay = 2f;

        public float BackgroundPanelMoveDelay = 0.5f;
        public float BackgroundPanelInitializePosY = 15f, BackgroundPanelStartPosY = 8.1f;

        public float GoTextFadeTime = 0.2f;
        public float GoTextShowTime = 0.4f;

        public Color32[] LevelColors;
    }
}