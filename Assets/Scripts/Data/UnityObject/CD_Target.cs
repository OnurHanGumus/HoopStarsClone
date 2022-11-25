using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Target", menuName = "Picker3D/CD_Target", order = 0)]
    public class CD_Target : ScriptableObject
    {
        public TargetData Data;
    }
}