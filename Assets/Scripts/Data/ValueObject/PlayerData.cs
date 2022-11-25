using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public float ForceX = 10, ForceY = 10;
        public float MaxHorizontalPoint = 10;
        public float MaxVerticalPoint = 5;
        public float InitializePosX = -1.4f, InitializePosY = 1.2f;
    }
}