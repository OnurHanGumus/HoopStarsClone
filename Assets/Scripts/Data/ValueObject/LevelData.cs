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
    }
}