using UnityEngine;

namespace Scripts
{
    public class GlobalSettings : MonoBehaviour
    {
        [SerializeField] private int _levelsCount;

        public int LevelsCount => _levelsCount;

        public JumpSettings JumpSettings;

        public float ScrollSpeed { get; set; }

        public float PlayerSpeed { get; set; }
    }
}