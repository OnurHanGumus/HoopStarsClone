using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class UIData
    {
        //public float  = 10, ForceY = 10;
        public float[] SliderValues = {0f, 0.3f, 0.7f, 1f, 1f};
        public Color32 DefaultStageColor, SuccessStageColor, FailStageColor;
    }
}