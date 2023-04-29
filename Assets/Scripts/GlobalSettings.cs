using UnityEngine;

namespace Scripts
{
    public class GlobalSettings : MonoBehaviour
    {
        public JumpSettings JumpSettings;

        public float ScrollSpeed => _scrollSpeed;

        [SerializeField, Range(0, 5f)]
        private float _scrollSpeed;

        public float PlayerSpeed { get; set; }
    }
}