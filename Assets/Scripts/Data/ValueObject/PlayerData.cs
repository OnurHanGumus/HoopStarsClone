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
    }
}