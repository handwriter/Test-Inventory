using UnityEngine;

namespace _Project.Develop.Runtime.Data
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Config")]
    public class LevelConfig : ScriptableObject
    {
        public float SnapSmoothtime;
        public float UISmoothtime;
        public float BackpackHoldTime;
        public float SnapDistance;
    }

}