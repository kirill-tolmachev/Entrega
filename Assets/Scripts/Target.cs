using Assets.Scripts;
using Assets.Scripts.MessageImpl;
using Assets.Scripts.Util;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using Zenject;

namespace Scripts
{
    internal class Target : MonoBehaviour
    {
        private bool _isActiveTarget = true;
        [SerializeField] private LayerMask _packageLayerMask;
        [SerializeField] private float _shrinkDuration;

        [Inject] private IMessageBus _messageBus;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isActiveTarget)
                return;

            if (!_packageLayerMask.HasLayer(other.gameObject.layer))
                return;

            if (!other.TryGetComponent(out Package package))
            {
                return;
            }

            package.FoundTarget = true;
            _isActiveTarget = false;

            var scale = transform.localScale;
            transform.DOScale(new Vector3(scale.x, 0f, scale.z), _shrinkDuration);

            _messageBus.Publish(new PackageDeliveredMessage(package, this)).Forget();
        }

    }
}
