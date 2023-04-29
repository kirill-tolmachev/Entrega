using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(menuName = "Create Jump settings")]
    public class JumpSettings : ScriptableObject
    {
        [SerializeField] private AnimationCurve _curve;

        [SerializeField] private float _totalTime;

        public float GetPosition(float time)
        {
            float normTime = time / _totalTime;
            return _curve.Evaluate(normTime);
        }
    }
}