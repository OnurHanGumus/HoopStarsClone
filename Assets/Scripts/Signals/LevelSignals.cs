using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public Func<int> onGetCurrentModdedLevel = delegate { return 0; };
        public UnityAction onTimeUp = delegate { };
        public UnityAction onTournamentWon = delegate { };
        public UnityAction onFinalStage = delegate { };
        public UnityAction onBasket = delegate { };
        public Func<Transform> onGetTransform = delegate { return null; };
    }
}