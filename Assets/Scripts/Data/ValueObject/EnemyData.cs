using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyData
    {
        public float ForceX = 10, ForceY = 10;
        public float MaxHorizontalPoint = 10;
        public float MaxVerticalPoint = 5;
        public float InitializePosX = 3f, InitializePosY = 1.2f;
        public float StartPosX = 1.4f, StartPosY = 1.2f;

        public float JumpDelay = 0.5f;
        public float EnemyInitializeAnimDelay = 0.5f;

    }
}