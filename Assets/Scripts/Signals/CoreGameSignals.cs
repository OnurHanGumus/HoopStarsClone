using System;
using Enums;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onStageFailed = delegate { };
        public UnityAction onStageSuccessful = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onPlayPressed = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };

        public UnityAction onSetCameraTarget = delegate { };

        public Func<int> onGetLevelID = delegate { return 0; };
    }
}