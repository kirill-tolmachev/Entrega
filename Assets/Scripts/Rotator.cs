using DG.Tweening;
using UnityEngine;
using UnityEngine.U2D;

namespace Scripts
{
    internal class Rotator : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        void Start()
        {
            transform.DOLocalRotate(new Vector3(0.0f, 0.0f, 90f), 0.12f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }
    }
}
